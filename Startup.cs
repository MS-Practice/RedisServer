using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RedisServer {
    public class Startup {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services
                .AddMvc()
                .AddNewtonsoftJson()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDistributedRedisCache(options => {
                options.Configuration = Configuration.GetConnectionString("RedisAddress");
            });
            services.AddSwaggerGen(options => {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc(SwaggerConsts.ApiName, new Microsoft.OpenApi.Models.OpenApiInfo {
                    Title = SwaggerConsts.DocTitle,
                        Version = SwaggerConsts.DocVersion
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            var pathBase = Environment.GetEnvironmentVariable("ASPNETCORE_PATHBASE");
            if (!string.IsNullOrEmpty(pathBase)) {
                app.UsePathBase(new PathString(pathBase));
                Console.WriteLine("Hosting PathBase: " + pathBase);
            }

            app.UseRouting();
            app.UseCors();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
            //app.UseEndpoints(endpoints => {
            //    endpoints.MapGet("/", async context => {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
            #region 
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint(pathBase + SwaggerConsts.EndpointUrl, SwaggerConsts.ApiName);
            });
            #endregion
        }
    }
}