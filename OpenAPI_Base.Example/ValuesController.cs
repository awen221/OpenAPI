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
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(int a, int b)
        {
            return Ok(a + b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(int a, int b)
        {
            return Ok(a - b);
        }
    }
}
