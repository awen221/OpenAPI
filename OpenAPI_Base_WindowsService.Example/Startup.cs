namespace OpenAPI_Base_WindowsService.Example
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.OpenApi.Models;
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup : OpenAPI_Base.Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) : base(configuration)
        {
        }
        /// <summary>
        /// Name
        /// </summary>
        protected override string Name => "Name";
        /// <summary>
        /// OpenApiInfo
        /// </summary>
        protected override OpenApiInfo OpenApiInfo => new OpenApiInfo()
        {
            Title = "Title",
            Version = "1.0",
        };
    }
}
