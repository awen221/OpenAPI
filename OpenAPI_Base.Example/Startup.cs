namespace OpenAPI_Base.Example
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.OpenApi.Models;

    public class Startup : OpenAPI_Base.Startup
    {
        public Startup(IConfiguration configuration) : base(configuration) { }

        protected override string Name => "Name";

        protected override OpenApiInfo OpenApiInfo => new OpenApiInfo()
        {
            Title = "Title",
            Version = "1.0",
        };
    }
}
