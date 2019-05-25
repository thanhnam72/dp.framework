using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace Tef.Project.V1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            EtonWebHost(args).Run();
        }

        public static IWebHost EtonWebHost(string[] args)
        {
            var webHostBuilder = new WebHostBuilder();
            webHostBuilder
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration(
                    (hostingContext, config) =>
                    {
                        IHostingEnvironment hostingEnvironment = hostingContext.HostingEnvironment;
                        config.AddJsonFile("appsettings.json", true, true).AddJsonFile(
                                string.Format("appsettings.{0}.json", (object)hostingEnvironment.EnvironmentName),
                                true, true)
                            .AddJsonFile("secrets/appsettings.secrets.json", optional: true);
                        if (hostingEnvironment.IsDevelopment())
                        {
                            Assembly assembly = Assembly.Load(new AssemblyName(hostingEnvironment.ApplicationName));
                            if (assembly != (Assembly)null)
                                config.AddUserSecrets(assembly, true);
                        }

                        config.AddEnvironmentVariables();
                        if (args == null)
                            return;
                        config.AddCommandLine(args);
                    })
                .UseStartup<Startup>();

            return webHostBuilder.Build();
        }
    }
}
