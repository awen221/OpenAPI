1.  建立專案"空的ASP.NET Core"  
架構：NET6.0  
（　）針對HTTPS進行設定  
（　）啟用Docker  
（　）Do not use top-level statements

1. 已設定在SwaggerUI頁面上輸出函式註解，需做以下設定否則會執行異常：  
專案屬性->建置->輸出->文件檔案  
（Ｖ）產生包含API文件的檔案。
1. 加入OpenAPI的專案參考
1. 覆寫Program如下：
    ```
    new StartUp().Main(args);
    /// <summary>
    /// StartUp
    /// </summary>
    public class StartUp : OpenAPI.StartUp
    {
        /// <summary>
        /// Title
        /// </summary>
        protected override string Title => "OpenAPI";
        /// <summary>
        /// Version
        /// </summary>
        protected override Version Version => new Version("1.0.0.0");
        /// <summary>
        /// Description
        /// </summary>
        protected override string Description => "OpenAPI Description";

        /// <summary>
        /// WebApplication_Process
        /// </summary>
        /// <param name="app"></param>
        protected override void WebApplication_Process(WebApplication app)
        {
            base.WebApplication_Process(app);
        }
        /// <summary>
        /// WebApplicationBuilder_Process
        /// </summary>
        /// <param name="builder"></param>
        protected override void WebApplicationBuilder_Process(WebApplicationBuilder builder)
        {
            base.WebApplicationBuilder_Process(builder);
        }
    }
    ```