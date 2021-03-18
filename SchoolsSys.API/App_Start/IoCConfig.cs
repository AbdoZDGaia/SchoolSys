using Autofac;
using Autofac.Integration.WebApi;
using SchoolsSys.BL.Repository;
using SchoolsSys.BL.UnitOfWork;
using System.Reflection;
using System.Web.Http;

namespace SchoolSys.API.App_Start
{
    public class IoCConfig
    {
        public static void Init(HttpConfiguration configuration)
        {
            Init(configuration, RegisterServices(new ContainerBuilder()));
        }

        public static void Init(HttpConfiguration configuration, IContainer container)
        {
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerDependency();
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>)).InstancePerDependency();
            builder.RegisterType(typeof(StudentsRepository)).As(typeof(IStudentsRepository)).InstancePerDependency();
            builder.RegisterType(typeof(AttachmentsRepository)).As(typeof(IAttachmentsRepository)).InstancePerDependency();

            var Container = builder.Build();
            return Container;
        }
    }
}