using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Test13
{
    public class DTest
    {
        [Log]
        public void LogTest()
        {
            HttpContext.Current.Response.Write("dddssss");
        }

        public void Test()
        {
            
        }
    }
}