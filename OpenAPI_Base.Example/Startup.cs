namespace OpenAPI_Base.Example
{
    using Microsoft.OpenApi.Models;
    /// <summary>
    /// 
    /// </summary>
    public class Startup : OpenAPI_Base.Startup
    {
        /// <summary>
        /// 
        /// </summary>
        protected override bool AlwaysUseSwaggerUI => base.AlwaysUseSwaggerUI;
        /// <summary>
        /// 
        /// </summary>
        protected override string Name => base.Name;
        /// <summary>
        /// 
        /// </summary>
        protected override OpenApiInfo OpenApiInfo => base.OpenApiInfo;
    }
}
