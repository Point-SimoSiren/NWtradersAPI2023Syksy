using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NWtradersAPI.Models;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace NWtradersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        // Kannattaa esitellä tietokantakonteksti kertaalleen täällä päätasolla,
        // niin metodeissa voi viitata suoraan db muuttujaan. Disposea ei tarvise tehdä.
        // Perinteinen tyyli jota voi käyttää ihan hyvin tässä vaiheessa vielä:
        // private NorthwindContext db = new NorthwindContext();

        // Dependency Injektion tyyli joka neuvotaan React kurssin lopulla, mutta teimme sen jo nyt tunneilla:
        // kts. myös program.cs injektion tekeminen ja appsettings.json connection string
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

        // Uuden asiakkaan lisääminen
        [HttpPost]
        public ActionResult AddNew([FromBody] Customer asiakas)
        {
            try
            {
                db.Customers.Add(asiakas);
                db.SaveChanges();
                return Ok($"Added new Customer {asiakas.CompanyName}.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error happened: {ex.InnerException}.");
            }
        }

        // Asiakkaan poistaminen
        [HttpDelete("{id}")] // URL parametrina id eli pääavain
        public ActionResult Remove(string id)
        {
                try
                {
                var poistettavaAsiakas = db.Customers.Find(id);
                if (poistettavaAsiakas != null)
                    {
                        db.Customers.Remove(poistettavaAsiakas);
                        db.SaveChanges();
                        return Ok($"Customer {poistettavaAsiakas.CompanyName} was deleted.");
                    }
                    else
                    {
                        return NotFound($"No Customer found with id {id}");
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest($"Error happened: {ex.InnerException}.");
                }
            }

        // Asiakkaan muokkaaminen
        [HttpPut("{id}")] // URL parametrina id eli pääavain
        public ActionResult Update(string id, [FromBody] Customer cust)
        {
            try
            {
                var muokattavaAsiakas = db.Customers.Find(id);
                if (muokattavaAsiakas != null)
                {
                   
                    muokattavaAsiakas.CustomerId = id; // Varmistetaan että id:tä ei muuteta
                    muokattavaAsiakas.CompanyName = cust.CompanyName;
                    muokattavaAsiakas.ContactName = cust.ContactName;
                    muokattavaAsiakas.ContactTitle = cust.ContactTitle;
                    muokattavaAsiakas.Address = cust.Address;
                    muokattavaAsiakas.PostalCode = cust.PostalCode;
                    muokattavaAsiakas.City = cust.City;
                    muokattavaAsiakas.Country = cust.Country;
                    muokattavaAsiakas.Phone = cust.Phone;
                    muokattavaAsiakas.Fax = cust.Fax;
                     
                    db.SaveChanges();
                    return Ok($"Customer {muokattavaAsiakas.CompanyName} was updated.");
                }
                else
                {
                    return NotFound($"No Customer found with id {id}");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Error happened: {ex.InnerException}.");
            }
        }

    }
}
