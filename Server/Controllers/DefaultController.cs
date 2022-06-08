using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProgettoIDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        private IHostEnvironment env { get; }

        public DefaultController(IHostEnvironment env)
        {
            this.env = env;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { ContentRootPath = this.env.ContentRootPath, Ambiente = this.env.EnvironmentName });
        }

    }
}
