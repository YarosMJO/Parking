using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Parking;

namespace ParkingApi.Controllers
{
    [Produces("application/json")]
    [Route("api/transactions")]
    public class TransactionsController : Controller
    {
        [HttpGet]
        [Route("transaction_logs")]
        public ActionResult GetTransactions()
        {
            return Ok(Parking.Parking.ReadTransactions());
        }

        [HttpGet]
        [Route("current_transactions")]
        public ActionResult GetCurrentTransactions()
        {
            List<Transaction> transactions = Parking.Parking.Transactions;
            if (transactions.Count == 0) return Ok("There are no transactions on parking yet");

            return Ok(transactions);
        }

        [HttpGet]
        [Route("car_current_transactions")]
        public ActionResult GetCarCurrentTransactions(int id)
        {
            List<Transaction> carTransactions = Parking.Parking.Transactions.FindAll(tr => tr.CarId == id);
            if (carTransactions == null) return Ok("There are no transactions for this car yet");

            return Ok(carTransactions);
        }

        [HttpPut]
        [Route("put")]
        public ActionResult PutMoney(int id, int balance)
        {
            Car dcar = Parking.Parking.Cars.Find(car => car.Id == id);
            if (dcar == null) return Ok("No such car on parking");
            dcar.Balance += balance;

            return Ok(dcar);
        }

    }
}
