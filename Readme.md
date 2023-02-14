1.  建立"空的ASP.NET Core"專案
<br>
架構：NET6.0
<br>
（　）針對HTTPS進行設定
<br>
（　）啟用Docker
<br>
<br>
（Ｖ）Do not use top-level statements

1. 已設定在SwaggerUI頁面上輸出函式註解，需做以下設定否則會執行異常：
<br>
專案屬性->建置->輸出->文件檔案
<br>
（Ｖ）產生包含API文件的檔案。

1. 加入OpenAPI的專案參考

1. 覆寫Program如下：
    ```
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) => new StartUp().Main(args);
    }

    /// <summary>
    /// StartUp
    /// </summary>
    public class StartUp : global::OpenAPI.StartUp
    {
        /// <summary>
        /// 
        /// </summary>
        protected override string Title => throw new NotImplementedException();
        /// <summary>
        /// 
        /// </summary>
        protected override Version Version => throw new NotImplementedException();
        /// <summary>
        /// 
        /// </summary>
        protected override string Description => throw new NotImplementedException();

        /// <summary>
        /// WebApplicationBuilder_Process
        /// </summary>
        /// <param name="builder"></param>
        protected override void WebApplicationBuilder_Process(WebApplicationBuilder builder)
        {
            base.WebApplicationBuilder_Process(builder);
        }
        /// <summary>
        /// WebApplication_Process
        /// </summary>
        /// <param name="app"></param>
        protected override void WebApplication_Process(WebApplication app)
        {
            base.WebApplication_Process(app);
        }
    }
    ```