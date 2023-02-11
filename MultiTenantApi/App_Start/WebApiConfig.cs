using MultiTenantApi.Common;
using MultiTenantApi.Ioc;
using System;
using System.Configuration;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Unity;

namespace MultiTenantApi
{
    public static class WebApiConfig
    {
        // Web API configuration and services
        public static void Register(HttpConfiguration config)
        {
            
            //Dependency Injection
            config.DependencyResolver = new UnityResolver(BuildUnityContainer());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            IocRegistration.RegisterUnitMappings(container);
            return container;
        }
    }

    // This is the Class for choose Teanant based in URL
    public class RoutingConstraint : IRouteConstraint 
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            //assuming URL pattern like tenant1.trackbox.wns.com    tenant2.trackbox.wns.com    
            return true;
        }
    }

}
