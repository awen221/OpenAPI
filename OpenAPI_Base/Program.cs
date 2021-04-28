using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace OpenAPI_Base
{

    public class Program
    {

        public static void Main<TStartup>(string[] args) where TStartup : Startup
        {
            CreateHostBuilder<TStartup>(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder<TStartup>(string[] args) where TStartup : Startup =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<TStartup>();
            });

    }

}

#region Example
/*
public class Program { static void Main(string[] args) => OpenAPI_Base.Program.Main<Startup>(args); }
*/
#endregion
