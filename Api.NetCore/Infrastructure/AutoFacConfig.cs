using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Api.NetCore.Infrastructure
{
    public class AutoFacConfig
    {
        public static IContainer Configure(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyModules(typeof(AutoFacConfig).Assembly);
            builder.Populate(services);
            var container = builder.Build();
            return container;
        }
    }
}
