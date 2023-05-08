1.  建立專案"空的ASP.NET Core"  
    架構：NET6.0  
    （　）針對HTTPS進行設定  
    （　）啟用Docker  
    （　）Do not use top-level statements

1.  已設定在SwaggerUI頁面上輸出函式註解，需做以下設定否則會執行異常：  
    專案屬性->建置->輸出->文件檔案  
    （Ｖ）產生包含API文件的檔案。
1.  Edit Properties\launchSettings.json  
    insert this
    ```
    "launchUrl": "swagger",
    ```
1.  加入OpenAPI的專案參考
1.  覆寫Program如下：
    ```
    using Microsoft.OpenApi.Models;

    new StartUp().Main(args);

    /// <inheritdoc/>
    public class StartUp : OpenAPI.StartUp
    {
        /// <inheritdoc/>
        protected override Dictionary<string, OpenApiInfo> SwaggerDocs => new()
        {
            {
                "OpenAPI",new OpenApiInfo(){
                    Title="OpenAPI",
                    Version=new Version("1.0.0.0").ToString(),
                    Description="OpenAPI Function",
                }
            },
        };

        /// <inheritdoc/>
        protected override void WebApplicationBuilder_Process(WebApplicationBuilder builder) 
            => base.WebApplicationBuilder_Process(builder);
        /// <inheritdoc/>
        protected override void WebApplication_Process(WebApplication app)
            => base.WebApplication_Process(app);
    }
    ```