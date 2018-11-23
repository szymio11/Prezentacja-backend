using Autofac;
using Logic.Interfaces;

namespace Api.NetCore.Infrastructure.AutoFacModule
{
    public class LogicModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ILogic).Assembly)
                .AsImplementedInterfaces();
        }
    }
}