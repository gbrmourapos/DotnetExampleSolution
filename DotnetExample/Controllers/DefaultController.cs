using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace DotnetExample.Controllers
{
    [ApiController]
    [Route("/")]
    public class DefaultController : ControllerBase
    {
        public DefaultController(){}

        [HttpGet]
        public string Get()
        {
            Log.Information("[GET] The initial endpoint was called");
            return "The API is running";
        }
    }
}
