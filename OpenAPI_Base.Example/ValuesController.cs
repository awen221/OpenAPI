using Microsoft.AspNetCore.Mvc;

namespace OpenAPI_Base.Example
{
    /// <summary>
    /// ValuesController
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Add(int a, int b)
        {
            return Ok(a + b);
        }

        /// <summary>
        /// Sub
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Sub(int a, int b)
        {
            return Ok(a - b);
        }
    }
}
