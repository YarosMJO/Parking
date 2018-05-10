using System;

namespace Parking
{
    public class Transaction
    {
        public Transaction(int CarId, DateTime DateTimeTransaction, double WrittenOff_Funds)
        {
            this.CarId = CarId;
            this.DateTimeTransaction = DateTimeTransaction;
            this.WrittenOff_Funds = WrittenOff_Funds;
        }
       public DateTime DateTimeTransaction { get; set; }
       public int CarId { get; set; }
       public double WrittenOff_Funds { get; set; }
    }
}
