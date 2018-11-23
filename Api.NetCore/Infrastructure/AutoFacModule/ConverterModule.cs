using Autofac;
using AutoMapper;
using Module = Autofac.Module;

namespace Api.NetCore.Infrastructure.AutoFacModule
{
    public class ConverterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Startup).Assembly)
                .AsClosedTypesOf(typeof(ITypeConverter<,>)).AsSelf();
        }
    }
}