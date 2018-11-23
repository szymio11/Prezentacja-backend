using Api.NetCore.DataAccess;
using Autofac;

namespace Api.NetCore.Infrastructure.AutoFacModule
{
    public class DataContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Repository<>).Assembly)
                .AsImplementedInterfaces(); 
        }
    }
}