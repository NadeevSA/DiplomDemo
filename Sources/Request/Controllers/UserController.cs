namespace Request.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        /// <summary>
        /// Метод Get
        /// </summary>
        /// <returns>Hello</returns>
        [HttpGet]
        public string Get()
        {
            return "Hello user";
        }
    }
}
