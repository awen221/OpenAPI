using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OpenAPI
{
    /// <summary>
    /// StartUp
    /// </summary>
    abstract public class StartUp
    {
        /// <summary>
        /// SwaggerDocs
        /// </summary>
        abstract protected Dictionary<string, OpenApiInfo> SwaggerDocs { get; }

        /// <summary>
        /// WebApplicationBuilder_Process
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void WebApplicationBuilder_Process(WebApplicationBuilder builder) 
        {
            var services = builder.Services;
            // Add services to the container.
            services.AddControllers().AddJsonOptions(options =>
            {
                //讓JSON成員名稱大小寫不會被變動
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                //讓參數Enum列舉的值以變數名稱顯示
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            //設定Swagger WebApplicationBuilder
            services.AddSwaggerGen(options =>
            {
                foreach (var name in SwaggerDocs.Keys)
                {
                    options.SwaggerDoc(name, SwaggerDocs[name]);
                }

                #region UI 顯示函式註解：專案屬性->建置->輸出->勾選'XML 文件檔案'可在SwaggerUI頁面上輸出函式註解
                try
                {
                    var Assembly = System.Reflection.Assembly.GetEntryAssembly();
                    if (Assembly != null)
                    {
                        var xmlFile = $"{Assembly.GetName().Name}.xml";
                        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                        options.IncludeXmlComments(xmlPath);
                    }
                }
                catch
                {
                    throw new Exception("Swagger Error，XML檔案錯誤");
                }
                #endregion
            });
        }
        /// <summary>
        /// WebApplication_Process
        /// </summary>
        /// <param name="app"></param>
        protected virtual void WebApplication_Process(WebApplication app) { }

        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        public void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            WebApplicationBuilder_Process(builder);

            var app = builder.Build();
            WebApplication_Process(app);

            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(option => {
                    foreach (var name in SwaggerDocs.Keys)
                    {
                        option.SwaggerEndpoint($"{name}/swagger.json", $"{name}");
                    }
                });
            }

            #region 這裡勿調動，容易影響正常執行
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
            #endregion
        }
    }
}