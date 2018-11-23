using System;
using System.Linq;
using Autofac;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Api.NetCore.Infrastructure.AutoFacModule
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            var profiles = GetType().Assembly.GetTypes()
                .Where(t => typeof(Profile).IsAssignableFrom(t))
                .Select(t => (Profile) Activator.CreateInstance(t));
            
            builder.Register(ctx => new MapperConfiguration(cfg =>
                {
                    cfg.ConstructServicesUsing(t => AutoFacConfig.Configure(new ServiceCollection()).Resolve(t));

                    foreach (var profile in profiles)
                    {
                        cfg.AddProfile(profile);
                    }
                }
            ));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
        }
    }
}