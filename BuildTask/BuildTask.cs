using System.IO;
using Aop;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using System.Linq;
using Mono.Cecil.Cil;

namespace BuildTask
{
    public class BuildTask : Task
    {
        [Required]
        public string OutputFile { get; set; }

        #region Overrides of Task

        public override bool Execute()
        {
            Log.LogWarning(OutputFile);
            BuildInterceptor(OutputFile);
            return true;
        }

        #endregion

        private void BuildInterceptor(string fileDir)
        {
            //var dirInfo = new DirectoryInfo(fileDir);
            //FileInfo[] files = dirInfo.GetFiles();

            //Log.LogMessage(files.Length.ToString());
            //if (files.Length == 0)
            //{
            //    return;
            //}

                Log.LogMessage("this sis a mest");
            //foreach (var fileInfo in files)
            //{
                //var assembly = AssemblyDefinition.ReadAssembly(fileInfo.FullName);

                //foreach (var item in assembly.MainModule.Types)
                //{
                //    var methods =
                //        item.Methods.Where(
                //            p => p.CustomAttributes.Any(s => s.GetType() == typeof (InterceptorAttribute)));

                //    foreach (var method in methods)
                //    {
                //        Log.LogWarning(method.FullName);
                //        var processor = method.Body.GetILProcessor();
                //        var instuction = method.Body.Instructions[0];

                //        var beforeMethod = assembly.MainModule.Import(typeof(InterceptorAttribute).GetMethod("OnExecuting"));
                //        var beforeInstruction = processor.Create(OpCodes.Call, beforeMethod);

                //        var afterMethod = assembly.MainModule.Import(typeof(InterceptorAttribute).GetMethod("OnExecuted"));
                //        var afterInstruction = processor.Create(OpCodes.Call, afterMethod);

                //        processor.InsertBefore(instuction, beforeInstruction);
                //        processor.InsertAfter(instuction, afterInstruction);
                //    }
                //}
            //}
        }
    }
}