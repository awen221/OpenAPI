using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;

namespace OpenAPI_Base
{
    public class Startup : StartUp_interface
    {
        virtual protected bool AlwaysUseSwaggerUI => true;
        virtual protected string Name => nameof(OpenAPI_Base);
        virtual protected OpenApiInfo OpenApiInfo => new OpenApiInfo()
        {
            Title = nameof(OpenApiInfo),
            Version = new Version().ToString(),
        };

        class EnumSchemaFilter : ISchemaFilter
        {
            public void Apply(OpenApiSchema model, SchemaFilterContext context)
            {
                if (context.Type.IsEnum)
                {
                    model.Enum.Clear();
                    Enum.GetNames(context.Type)
                        .ToList()
                        .ForEach(name => model.Enum.Add(new OpenApiString($"{name} = {Convert.ToInt64(Enum.Parse(context.Type, name))}")));
                }
            }
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                //Add下列Func可直接回傳DataTable
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Name, OpenApiInfo);
                #region UI 顯示函式註解：專案屬性->建置->輸出->勾選'XML 文件檔案'可在SwaggerUI頁面上輸出函式註解
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
                #region UI Enum宣告處顯示Enum成員名稱而非僅數值
                c.SchemaFilter<EnumSchemaFilter>();
                #endregion
            });
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            void SetSwaggerUI(IApplicationBuilder app, IWebHostEnvironment env)
            {
                #region 設立AlwaysUseSwaggerUI變數控制
                if (!AlwaysUseSwaggerUI)
                {
                    if (!env.IsDevelopment()) return;
                }
                #endregion

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint($"swagger/{Name}/swagger.json", Name);

                    //將預設結尾路徑"\swagger"清空，偵錯時可直接進入SwaggerUI頁面，不用另加設定結尾路徑"\swagger"
                    c.RoutePrefix = string.Empty;
                });
            }

            SetSwaggerUI(app, env);

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
interface StartUp_interface
{
    void ConfigureServices(IServiceCollection services);
    void Configure(IApplicationBuilder app, IWebHostEnvironment env);
}

#region Example
/*        
    using Microsoft.OpenApi.Models;
    /// <summary>
    /// 
    /// </summary>
    public class Startup : OpenAPI_Base.Startup
    {
        /// <summary>
        /// 
        /// </summary>
        protected override bool AlwaysUseSwaggerUI => base.AlwaysUseSwaggerUI;
        /// <summary>
        /// 
        /// </summary>
        protected override string Name => base.Name;
        /// <summary>
        /// 
        /// </summary>
        protected override OpenApiInfo OpenApiInfo => base.OpenApiInfo;
    }
*/
#endregion