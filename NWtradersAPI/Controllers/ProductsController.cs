using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWtradersAPI.Models;

namespace NWtradersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Dependency Injektion tyyli:
        private NorthwindContext db;
        public ProductsController(NorthwindContext dbparam)
        {
            db = dbparam;
        }


        // Hakee kaikki
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var p = db.Products.ToList();
                return Ok(p);
            }
            catch (Exception ex)
            {
                return BadRequest("Tuli virhe: " + ex.GetType().FullName + " - " + ex.InnerException);
            }
        }
    }
}
