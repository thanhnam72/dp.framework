using DP.V2.Core.Data;
using DP.V2.Core.Data.Interface;
using DP.V2.Core.Data.UoW;
using DP.V2.Core.WebApi.Dependencies;
using DP.V2.Core.WebApi.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DP.V2.Core.WebApi.Configuration
{
    public class WebApiStartup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public static void ApiConfigureServices(IServiceCollection services, IConfiguration config)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ApiAuthentication));
            })
            .AddFluentValidation(configs =>
            {
                configs.RegisterValidatorsFromAssemblyContaining<WebApiStartup>();
                configs.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddHttpContextAccessor();
            
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddSingleton<IConfiguration>(config);

            System.Web.HttpContext.Configure(services);

            DependencyProvider.Configure();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "default",
                  template: "{controller=Index}/{action=Index}/{id?}"
                );
            });
        }
    }
}
