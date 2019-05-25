using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DP.V2.Core.WebApi.Configuration;
using DP.V2.Core.Data;
using Tef.Data;
using Microsoft.EntityFrameworkCore;

namespace Tef.Project.V1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            WebApiStartup.ApiConfigureServices(services, Configuration);
            services.AddDbContext<TEFContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );
            services.AddScoped(typeof(DataContext), typeof(TEFContext));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (AppSetting.GetObject<bool>("CreateDb"))
            {
                var context = app.ApplicationServices
                    .GetService<IServiceScopeFactory>()
                    .CreateScope()
                    .ServiceProvider
                    .GetRequiredService<TEFContext>();

                context.Database.EnsureCreated();
            }

            WebApiStartup.Configure(app, env);
        }
    }
}
