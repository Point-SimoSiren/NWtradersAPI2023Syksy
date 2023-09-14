using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NWtradersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public ActionResult Hello()
        {

            return Ok("Hello World From .NET6 Rest API!");

        }

    }
}
