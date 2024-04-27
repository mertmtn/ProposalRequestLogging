using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProposalRequestLogging.Business.Abstract;
using ProposalRequestLogging.Business.Concrete;
using ProposalRequestLogging.Data.Abstract;
using ProposalRequestLogging.Data.Concrete.Dapper;
using System;
using System.Net.Http.Headers;

namespace ProposalRequestLogging.Web
{
    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.AddSingleton<IRequestLogService, RequestLogManager>();
            services.AddSingleton<IRequestLogsDal, DapperRequestLogsDal>();

            services.AddHttpClient("apiClient", estClient =>
            {
                estClient.BaseAddress = new Uri(Configuration.GetSection("ApiInformations").GetSection("BaseUrl").Value);
                estClient.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });   
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {        
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
