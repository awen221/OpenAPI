namespace OpenAPI_Base_WindowsService
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;
    /// <summary>
    /// Program
    /// </summary>
    public class Program : OpenAPI_Base.Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        new public static void Main<TStartup>(string[] args) where TStartup : OpenAPI_Base.Startup
        {
            CreateHostBuilder<TStartup>(args).Build().Run();
        }

        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <typeparam name="TStartup"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        new public static IHostBuilder CreateHostBuilder<TStartup>(string[] args) where TStartup : OpenAPI_Base.Startup =>
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<TStartup>();
            });
    }
}

#region Example
/*
    using static OpenAPI_Base_WindowsService.Program;
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) => Main<Startup>(args);
    }
*/
#endregion
