using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RedisServer.Utilities;

namespace RedisServer.Applications.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase {
        private readonly RedisManager _manager;
        public HomeController(IConfiguration configuration) {
            _manager = new RedisManager(configuration.GetConnectionString("RedisAddress"));
        }

        [HttpGet]
        public IActionResult Index() {
            var pathBase = Environment.GetEnvironmentVariable("ASPNETCORE_PATHBASE");
            return Redirect(pathBase + "/swagger");
        }
    }
}