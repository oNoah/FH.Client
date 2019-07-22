using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace OrderAPI
{
    /// <summary>
    /// 启动项
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

   

        /// <summary>
        /// 配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            services.AddMvcCore()
                .AddApiExplorer()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication( options =>
                {
                    
                    options.Authority = Environment.GetEnvironmentVariable("OAUTH_ISSUER");
                    options.RequireHttpsMetadata = false;

                    options.ApiName = Configuration["OAuthClient:ApiName"];
                });

         

            // swagger 配置 右键项目属性 生成选项卡中,先选择Out put path,再 选中xml documentation file
            // 输出路径： bin\
            // xml路径：  bin\OrderAPI.xml
            // NuGet添加 Swashbuckle.AspNetCore 
            var xmlPath = Path.Combine(AppContext.BaseDirectory, "OrderAPI.xml");

            services.AddSwaggerCore(new SwaggerServiceGenOptions
            {
                Title = "订单服务文档",
                Version = "Version 1.0",
                Description = "FH.OrderService",
                XmlPath = xmlPath,
                Issuer = Environment.GetEnvironmentVariable("OAUTH_ISSUER")
        });

            // 注册网关

            // 注册事件


        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            app.UseSwaggerCore(new SwaggerServiceUIOptions
            {
                ProjectName = "Order Service",
                ClientId = Configuration["OAuthClient:Swagger:ClientId"],
                ClientSecret = Configuration["OAuthClient:Swagger:ClientSecret"]
            });
        }
    }
}
