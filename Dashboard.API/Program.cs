using Dashboard.API.Application.Infrastructure.Identity;
using Dashboard.API.Infrastructure.Identity;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Dashboard.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                    services.AddTransient<IValidateAuthToken, AmazonValidateAuthToken>())
                .UseStartup<Startup>()
                .Build();
    }
}
