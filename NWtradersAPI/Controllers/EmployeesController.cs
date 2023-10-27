using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWtradersAPI.Models;

namespace NWtradersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        // Dependency Injektion tyyli:
        private NorthwindContext db;
        public EmployeesController(NorthwindContext dbparam)
        {
            db = dbparam;
        }


        [HttpGet]
        public ActionResult GetEmp() { 
            var e = db.Employees.ToList();
            return Ok(e);
        }







    }
}
