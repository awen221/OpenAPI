using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace OpenAPI_Base
{

    abstract public class Startup
    {

        abstract protected string Name { get; }

        abstract protected OpenApiInfo OpenApiInfo { get; }

        virtual protected bool AlwaysUseSwaggerUI => true;


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Name, OpenApiInfo);


                #region 專案屬性->建置->輸出->勾選'XML 文件檔案'可在SwaggerUI頁面上輸出函式註解
                try
                {
                    var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                }
                catch
                {
                    //未輸出Xml時不跳異常
                }
                #endregion

                //UI Enum宣告處顯示成員名稱
                c.SchemaFilter<EnumSchemaFilter>();

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            bool useSwaggerUI = env.IsDevelopment();
            if (AlwaysUseSwaggerUI) useSwaggerUI = true;

            if (useSwaggerUI)
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("swagger/" + Name + "/swagger.json", OpenApiInfo.Title + " " + OpenApiInfo.Version);
                    //將預設結尾路徑"\swagger"清空
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }

}

#region Example
/*        
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;

public class Startup : OpenAPI_Base.Startup
{
    public Startup(IConfiguration configuration) : base(configuration) { }

    protected override string Name => "Name";

    protected override OpenApiInfo OpenApiInfo => new OpenApiInfo()
    {
        Title = "Title",
        Version = "1.0",
    };
}
*/
#endregion