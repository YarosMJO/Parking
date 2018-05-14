using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Parking;

namespace ParkingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/cars")]
    public class CarsController : Controller
    {
        [HttpGet]
        public ActionResult GetCars()
        {

            if(Parking.Parking.Cars.Count == 0)
                return Ok("No cars on parking");

            return Ok(JsonConvert.SerializeObject(Parking.Parking.Cars));
        }
        // GET: api/cars/5
        [HttpGet("{id:int}", Name = "Get")] 
        public ActionResult GetCar(int id)
        {
            
            Car dcar = Parking.Parking.Cars.Find(car => car.Id == id);
            if (dcar == null) return Ok("No such car on parking");

            return Ok(dcar);
        }

        
        // POST: api/cars/add 
        [HttpPost]
        [Route("add")]
        public ActionResult AddCar(int id, CarTypes type, int balance=0)
        {
            Car car = new Car()
            {
                Id = id,
                Type = type,
                Balance = balance             
            };

            return Ok(Parking.Parking.AddCar(car));
        }
        // DELETE: api/cars/delete 
        [HttpDelete]
        [Route("delete")]
        public ActionResult DeleteCar(int id)
        {
            if (Parking.Parking.Cars.Any(car => car.Id == id))
                return Ok(JsonConvert.SerializeObject(Parking.Parking.RemoveCar(id)));
            else return Ok("No such car on parking");
        }
    }
}
