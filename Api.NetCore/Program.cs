using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Api.NetCore
{
    public class Program
    {
    #pragma warning disable CS1591
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
    #pragma warning disable CS1591
}
