namespace Test13
{
    using System;
    using System.Web.UI;

    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Mytest();
        }

        [Log]
        public void Mytest()
        {
            Response.Write("mytest");
        }
    }
}