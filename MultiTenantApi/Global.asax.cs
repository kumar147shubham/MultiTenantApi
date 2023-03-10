using MultiTenantApi.Interfaces;
using MultiTenantApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace MultiTenantApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_AuthenticateRequest()
        {
            TokenService.AuthenticateRequst();
        }
    }
}
