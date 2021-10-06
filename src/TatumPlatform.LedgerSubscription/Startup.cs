using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TatumPlatform.LedgerSubscription
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddControllers();
            services.AddAuthentication(o => {
                o.DefaultScheme = SchemesNamesConst.TokenAuthenticationDefaultScheme;
            }).AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>(SchemesNamesConst.TokenAuthenticationDefaultScheme, o => { });
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("CheckHmacDigest", policy => policy.Requirements.Add(new HmacDigest("a21")));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/healthz");
                endpoints.MapControllerRoute(
                    name: "people",
                    pattern: "api/Ledger",
                    defaults: new { controller = "Ledger" });
                
                endpoints.MapGet("/", async context =>
                {
                    
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
