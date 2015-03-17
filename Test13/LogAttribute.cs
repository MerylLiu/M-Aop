namespace Test13
{
    using System.Web;
    using Aop;

    public class LogAttribute : InterceptorAttribute
    {
        public override void OnExecuting()
        {
            HttpContext.Current.Response.Write("start<br/>");
        }

        public override void OnExecuted()
        {
            HttpContext.Current.Response.Write("<br/>end");
        }
    }
}