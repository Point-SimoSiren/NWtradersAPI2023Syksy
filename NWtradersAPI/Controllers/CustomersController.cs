using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWtradersAPI.Models;
using System.Diagnostics.CodeAnalysis;

namespace NWtradersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Perinteinen tyyli:
        // private NorthwindContext db = new NorthwindContext();

        // Dependency Injektion tyyli:
        private NorthwindContext db;
        public CustomersController(NorthwindContext dbparam)
        {
            db = dbparam;
        }


        // Hakee kaikki
        [HttpGet]
        public ActionResult GetAll() {
            try
            {
                List<Customer> cust = db.Customers.ToList();
                return Ok(cust);
            }
            catch (Exception ex) {
                return BadRequest("Tuli virhe: " + ex.GetType().FullName + " - " + ex.InnerException);
            }
        }

        // Hakee 1 asiakkaan ID:n perusteella
        [HttpGet("{id}")]
        //tai:
        //  [HttpGet]
        // [Route("{id}")]
        public ActionResult Get1ById(string id)
        {
            try
            {
                Customer? cust = db.Customers.Find(id);
                return Ok(cust);
            }
            catch (Exception ex)
            {
                return BadRequest("Tuli virhe: " + ex.GetType().FullName + " - " + ex.InnerException);
            }
        }

        // Hakee nimen osalla
        [HttpGet("companyname/{cname}")]
        public ActionResult GetByName(string cname)
        {
            try
            {
                var cust = db.Customers.Where(c => c.CompanyName.Contains(cname));
                //var cust = from c in db.Customers where c.CompanyName.Contains(cname) select c; <-- sama mutta traditional
                // var cust = db.Customers.Where(c => c.CompanyName == cname); <--- perfect match
                return Ok(cust);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Hakee nimen osalla
        [HttpGet("country/{country}")]
        public ActionResult GetByCountry(string country)
        {
            try
            {
                var cust = db.Customers.Where(c => c.Country.ToLower() == country.ToLower());
                return Ok(cust);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
