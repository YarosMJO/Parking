using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        public ActionResult GetTransactions()// занести в паркінг
        {
            string sLine = "";
            ArrayList arrText = new ArrayList();
            using (StreamReader objReader = new StreamReader("Transaction.log"))
            {
                while (sLine != null)
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                        arrText.Add(sLine);
                }
            }
            return Ok(arrText);
        }

        [HttpGet]
        [Route("current_transactions")]
        public ActionResult GetCurrentTransaction()
        {
            List<Transaction> transactions = Parking.Parking.Transactions;
            if (transactions.Count == 0) return Ok("There are no transactions on parking yet");

            return Ok(transactions);
        }

        [HttpGet]
        [Route("current_transaction")]
        public ActionResult GetCarCurrentTransaction(int id)
        {
            List<Transaction> carTransactions =  Parking.Parking.Transactions.FindAll(tr => tr.CarId == id);
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
