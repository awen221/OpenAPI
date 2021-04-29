namespace OpenAPI_Base.Example
{
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
}
