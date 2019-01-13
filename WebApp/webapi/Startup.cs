using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hexagon.Data;
using Hexagon.Data.EF;
using Hexagon.Data.EF.Context;
using Hexagon.IService;
using Hexagon.Service;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace webapi
{
    public class Startup
    {
        private object configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(
              options => {
                  options.ReturnHttpNotAcceptable = true;
                  //格式协商:如果在header中确定Accept=application/xml，可以发送xml格式结果；默认是json格式结果
                  options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
              })
              .AddJsonOptions(options =>
              {
                  options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
              })
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //add https capability, to configure middleware options
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
                //default port value is 443
                options.HttpsPort = 6001;
            });

            //IdentityServerAuthenticationDefaults.AuthenticationScheme == "Bearer"
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetValue<string>("IdentityUrl");
                    options.ApiName = "restapi";
                });

            services.AddDbContext<SqliteDbContext>(options =>
            {
                options.UseSqlite("Data Source=eoc.db");
            });

            services.AddScoped<IOrgEmployee, OrgEmployee>();
            services.AddScoped<IBaseModuleService, BaseModuleService>();

            //配置跨域
            services.AddCors(options => {
                options.AddPolicy("AllowAngularDevOrigin",
                    builder => builder.WithOrigins("http://localhost:4200")
                    //header 带有X-Pagination
                    .WithExposedHeaders("X-Pagination")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
            });

            //对于所有controller需要认证用户才能访问
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAngularDevOrigin"));

                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //var options = new DefaultFilesOptions();
            //options.DefaultFileNames.Clear();
            //options.DefaultFileNames.Add("index.html");
            //app.UseDefaultFiles(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //added before UseHttpsRedirection
            app.UseCors("AllowAngularDevOrigin");

            //HTTPS Redirection Middleware (UseHttpsRedirection) to redirect HTTP requests to HTTPS.
            app.UseHttpsRedirection();            
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
