using FH.Swagger;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.IO;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerCore(this IServiceCollection services, SwaggerServiceGenOptions option)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc($"v1", new Info
                {
                    Title = option.Title,
                    Version = option.Version,
                    Description = option.Description
                });

                options.CustomSchemaIds(x => x.FullName);

                options.DescribeAllEnumsAsStrings();

                Console.WriteLine($"xmlpath: {option.XmlPath}");

                if (!File.Exists(option.XmlPath))
                    throw new FileNotFoundException("Xml文件不存在！");
                options.IncludeXmlComments(option.XmlPath);

                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "password",
                    AuthorizationUrl = option.Issuer + "/connect/authorize",
                    TokenUrl = option.Issuer + "/connect/token",
                });

                options.OperationFilter<AuthorizeCheckOperationFilter>();
            });

            return services;
        }


        public static IApplicationBuilder UseSwaggerCore(this IApplicationBuilder app, SwaggerServiceUIOptions option)
        {
            //启用Swagger
            app.UseSwagger();
            //启用SwaggerUI
            app.UseSwaggerUI(options =>
            {
                //文档终结点
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"configuration{option.ProjectName} API V1");
                //文档标题
                options.DocumentTitle = option.ProjectName;
                //页面API文档格式 Full=全部展开， List=只展开列表, None=都不展开
                options.DocExpansion(DocExpansion.List);

                options.OAuthClientId(option.ClientId);

                options.OAuthClientSecret(option.ClientSecret);
            });
            return app;
        }


    }

    public class SwaggerServiceGenOptions
    {
        /// <summary>
        /// 文档标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文档版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 文档描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// swagger xml文件地址 设置Swagger注释  需要 右键项目 -> 生成  -> 输出 -> 勾选XML文档文件 才会产生XML文件
        /// </summary>
        public string XmlPath { get; set; }

        /// <summary>
        /// 认证地址
        /// </summary>
        public string Issuer { get; set; }
    }

    public class SwaggerServiceUIOptions
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
    }
}
