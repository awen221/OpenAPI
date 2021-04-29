using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace OpenAPI_Base
{
    public class Program
    {
        static public IHostBuilder CreateHostBuilder<TStartup>(string[] args) where TStartup : class
            => Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<TStartup>();
            });
    }
}

#region Example
/*
    using Microsoft.Extensions.Hosting;
    /// <summary>
    /// 
    /// </summary>
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args) => OpenAPI_Base.Program.CreateHostBuilder<Startup>(args).Build().Run();
    }
*/
#endregion
