using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace OpenAPI
{
    /// <summary>
    /// 
    /// </summary>
    abstract public class StartUp
    {
        #region OpenApiInfo

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

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        protected virtual void WebApplicationBuilder_Process(WebApplicationBuilder builder) 
        {
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //讓JSON成員名稱大小寫不會被變動
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            //WebApplicationBuilder Swagger設定
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
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        protected virtual void WebApplication_Process(WebApplication app) 
        {
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    #region 略過swagger前綴路徑，
                    c.SwaggerEndpoint(
                        $"swagger/{OpenApiInfo.Version}/swagger.json",
                        $"{OpenApiInfo.Title} {OpenApiInfo.Version}"
                        );
                    c.RoutePrefix = string.Empty;
                    #endregion
                });
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        public void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            WebApplicationBuilder_Process(builder);

            var app = builder.Build();
            WebApplication_Process(app);
        }
    }
}