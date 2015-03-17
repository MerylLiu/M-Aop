using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace BuildTask
{
    public class MyBuildTask : Task
    {
        [Required]
        public string OutputFile { get; set; }

        #region Overrides of Task

        public override bool Execute()
        {
            var dirInfo = new DirectoryInfo(OutputFile);
            FileInfo[] files = dirInfo.GetFiles();

            foreach (var fileInfo in files.Where(p => p.FullName.EndsWith(".dll")))
            {
                //Log.LogWarning(fileInfo.FullName);
                var assembly = AssemblyDefinition.ReadAssembly(fileInfo.FullName);

                foreach (var item in assembly.Modules[0].Types)
                {
                    foreach (var method in item.Methods)
                    {
                        var attributes = method.CustomAttributes.Where(
                                t => t.AttributeType.Resolve().BaseType.Name == "InterceptorAttribute");

                        foreach (CustomAttribute attribute in attributes)
                        {
                            //var processor = method.Body.GetILProcessor();
                            //var instuction = method.Body.Instructions[0];

                            //var beforeMethod =
                            //    assembly.MainModule.Import(
                            //        attribute.AttributeType.Resolve()
                            //            .Methods.SingleOrDefault(p => p.Name == "OnExecuting"));
                            //var beforeInstruction = processor.Create(OpCodes.Call, beforeMethod);

                            //var afterMethod =
                            //    assembly.MainModule.Import(
                            //        attribute.AttributeType.Resolve()
                            //            .Methods.SingleOrDefault(p => p.Name == "OnExecuted"));
                            //var afterInstruction = processor.Create(OpCodes.Call, afterMethod);

                            //instuction = method.Body.Instructions[method.Body.Instructions.Count - 1];

                            //processor.InsertBefore(instuction, beforeInstruction);
                            //processor.InsertAfter(instuction, afterInstruction);

                            //var dd = processor.Create(OpCodes.Ldstr, "this a start teset");
                            //processor.InsertBefore(instuction, dd);

                            var resolve = attribute.AttributeType.Resolve();

                            var ilProcessor = method.Body.GetILProcessor();
                            var firstInstruction = ilProcessor.Body.Instructions.First();

                            var onActionBefore = resolve.Methods.Single(n => n.Name == "OnExecuting");
                            var mfReference1 = assembly.MainModule.Import(typeof(System.Reflection.MethodBase).GetMethod("GetCurrentMethod"));
                            ilProcessor.InsertBefore(firstInstruction, ilProcessor.Create(OpCodes.Call, mfReference1));
                            ilProcessor.InsertBefore(firstInstruction, ilProcessor.Create(OpCodes.Call, onActionBefore));

                            var onActionAfter = resolve.Methods.Single(n => n.Name == "OnExecuted");
                            var mfReference2 = assembly.MainModule.Import(typeof(System.Reflection.MethodBase).GetMethod("GetCurrentMethod"));
                            ilProcessor.InsertBefore(ilProcessor.Body.Instructions.Last(), ilProcessor.Create(OpCodes.Call, mfReference2));
                            ilProcessor.InsertBefore(ilProcessor.Body.Instructions.Last(), ilProcessor.Create(OpCodes.Call, onActionAfter));
                        }
                    }
                }

                assembly.Write(fileInfo.FullName);
            }

            return true;
        }

        #endregion
    }
}