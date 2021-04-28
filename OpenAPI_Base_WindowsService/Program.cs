namespace OpenAPI_Base_WindowsService
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Hosting;

    public class Program : OpenAPI_Base.Program
    {

        new public static void Main<TStartup>(string[] args) where TStartup : OpenAPI_Base.Startup
        {
            CreateHostBuilder<TStartup>(args).Build().Run();
        }

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
public class Program { public static void Main(string[] args) => OpenAPI_Base_WindowsService.Program.Main<Startup>(args); }
*/
#endregion
