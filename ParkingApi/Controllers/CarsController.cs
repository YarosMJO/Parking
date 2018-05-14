using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Parking;

namespace ParkingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/cars")]
    public class CarsController : Controller
    {
        // GET: api/cars
        [HttpGet]
        public ActionResult GetCars()
        {

            if (Parking.Parking.Cars.Count == 0)
                return NotFound("No cars on parking");

            return Ok(Parking.Parking.Cars);
        }

        // GET: api/cars/5
        [HttpGet("{id:int}", Name = "Get")]
        public ActionResult GetCar(int id)
        {

            Car dcar = Parking.Parking.Cars.Find(car => car.Id == id);
            if (dcar == null) return Ok("No such car on parking");

            return Ok(dcar);
        }

        // POST: api/cars
        [HttpPost]
        [Route("")]
        public ActionResult AddCar([FromBody]Car car)
        {
            if (Parking.Parking.AddCar(car)) return Ok("Car successfully added.");
            return Ok("Can't add car. Perhaps it already exist.");
        }
        // DELETE: api/cars/delete 
        [HttpDelete]
        [Route("")]
        public ActionResult DeleteCar(int id)
        {
            if (Parking.Parking.Cars.Any(car => car.Id == id))
            {
                Parking.Parking.RemoveCar(id);
                return NoContent();
            }
            else return NotFound("No such car on parking");
        }
    }
}
