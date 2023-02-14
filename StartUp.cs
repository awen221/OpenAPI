﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace OpenAPI
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class StartUp
    {
        /// <summary>
        /// 
        /// </summary>
        abstract protected string Title { get; }
        /// <summary>
        /// 
        /// </summary>
        abstract protected Version Version { get; }
        /// <summary>
        /// 
        /// </summary>
        abstract protected string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        OpenApiInfo OpenApiInfo => new()
        {
            Title = Title,
            Version = Version.ToString(),
            Description = Description,
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void WebApplicationBuilder_Process(WebApplicationBuilder builder) 
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        protected virtual void WebApplication_Process(WebApplication app) 
        { 
            app.UseAuthorization(); 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        public void Main(string[] args)
        {
            #region WebApplicationBuilder
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.

            builder.Services.AddControllers();
            //讓JSON成員名稱大小寫不會被變動
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(OpenApiInfo.Version, OpenApiInfo);

                #region UI 顯示函式註解：專案屬性->建置->輸出->勾選'XML 文件檔案'可在SwaggerUI頁面上輸出函式註解
                try
                {
                    var Assembly = System.Reflection.Assembly.GetEntryAssembly();
                    if (Assembly != null)
                    {
                        var xmlFile = $"{Assembly.GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        c.IncludeXmlComments(xmlPath);
                    }
                }
                catch
                {
                    throw new Exception("Swagger Error，XML檔案錯誤");
                }
                #endregion
            });

            WebApplicationBuilder_Process(builder);
            #endregion

            #region WebApplication
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    #region 略過swagger前綴路徑，
                    c.SwaggerEndpoint($"swagger/{OpenApiInfo.Version}/swagger.json", $"{OpenApiInfo.Title} {OpenApiInfo.Version}");
                    c.RoutePrefix = string.Empty;
                    #endregion
                });
            }

            WebApplication_Process(app);

            app.MapControllers();
            app.Run();
            #endregion
        }
    }
}