namespace Response.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class ResponseController : ControllerBase
    {
        private readonly ILogger<ResponseController> _logger;

        public ResponseController(ILogger<ResponseController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Метод Get
        /// </summary>
        /// <returns>Hello</returns>
        [HttpGet]
        public ActionResult<Task<String>> Get()
        {
            return Ok("Ok");
        }
    }
}