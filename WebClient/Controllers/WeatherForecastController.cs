using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebClient.Services.Impl;

namespace WebClient.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly  ITcpClientManager _clientManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITcpClientManager clientManager)
        {
            _logger = logger;
            _clientManager = clientManager;
        }

        [HttpGet]
        public async Task<IActionResult> StartClient()
        {
            var id =
                _clientManager.StartNew();

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> Send(string id, string msg)
        {
            await _clientManager.Send(id, msg);

            return Ok();
        }
    }
}
