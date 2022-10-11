using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

using System.Web.Http;
using Microsoft.Owin.Builder;

[assembly: OwinStartup(typeof(MiddleWare.Startup1))]

namespace MiddleWare
{
    public class Startup1
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}
