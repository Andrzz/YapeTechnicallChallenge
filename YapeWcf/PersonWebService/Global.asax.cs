using System;
using System.Web;

namespace PersonWebService
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AutofacConfig.RegisterDependencies();
        }
    }
}