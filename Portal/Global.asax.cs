using Autofac;
using Autofac.Integration.Mvc;
using DataAceess;
using DataAceess.Interfaces;
using DataAceess.Models;
using DataAceess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Portal
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            //haitham -- autofac init and reg
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);


            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();

            //how to support generics?
            //builder.RegisterAssemblyTypes(Assembly.Load(nameof(DataAceess)))
            //    .Where(t => t.Namespace.Contains("Repositories"))
            //    .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));
            builder.RegisterType<EmployesRepository>().As<IEmployeRepository>();
            builder.RegisterType<BaseRepository<Branch>>().As<IBaseRepository<Branch>>();
            builder.RegisterType<BaseRepository<FingerPrintDevice>>().As<IBaseRepository<FingerPrintDevice>>();
            builder.RegisterType<BaseRepository<AttendanceLog>>().As<IBaseRepository<AttendanceLog>>();

            builder.RegisterType<AttendanceEntities>().AsSelf();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
