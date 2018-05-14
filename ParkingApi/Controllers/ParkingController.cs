using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ParkingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/parking")]
    public class ParkingController : Controller
    {
        // GET: api/parking
        [HttpGet]
        [Route("total_balance")]
        public ActionResult GetTotalBalance()
        {
            return Ok(Parking.Parking.Balance);
        }

        [HttpGet]
        [Route("free_spaces")]
        public ActionResult GetFreeSpaces()
        { 
            return Ok(Parking.Parking.ParkingSpace);
        }

        [HttpGet]
        [Route("occupied_spaces")]
        public ActionResult GetOccupiedSpaces()
        {
            return Ok(Parking.Parking.ParkingSpace);//change to occupied
        }
    }
}
