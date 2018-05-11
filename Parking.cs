using System;
using System.Collections.Generic;
using System.Linq;

namespace Parking
{
    public sealed class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        public static Parking Instance
        {
            get { return lazy.Value; }
        }

        private static int parkingSpace;
        private static double balance;
        private static double interimBalance = 0;
       
        private static List<Car> cars = null;
        private static List<Transaction> transactions = null;

        public static int ParkingSpace { get { return parkingSpace; } private set { parkingSpace = value; } }
        public static double Balance { get { return balance; } set { balance = value; } }
        public static double CurrentBalance { get { return interimBalance; } set { interimBalance = value; } }

        public static List<Car> Cars { get { return cars; } private set { } }
        public static List<Transaction> Transactions { get { return transactions; } private set { } }


        static Parking()
        {
            Instance.InitParking();
        }

        private Parking(){}

        private void InitParking()
        {
            Settings.TimeOut = 3000;
            Settings.LogTime = 60000;
            ParkingSpace = Settings.ParkingSpace;
            cars = new List<Car>(ParkingSpace);
            transactions = new List<Transaction>();
        }

        public static bool AddCar(Car newCar)
        {
            if(Cars.Any(car => car.Id == newCar.Id))
            {
                Console.WriteLine("Such car already exists. Try another car(id).");
            }
            if (newCar != null && Settings.ParkingSpace > Cars.Count )
            {
                cars.Add(newCar);
                ParkingSpace--;
                Console.WriteLine("Car successfully added.");
                return true;
            }
            Console.WriteLine("Can't add new car");
            return false;
        }

        public static bool RemoveCar(int id)
        {

            foreach (Car car in cars)
            {
                if (car.Id == id && car.Balance > Settings.carPrices[Convert.ToString(car.Type)])
                {
                    cars.Remove(car);
                    ParkingSpace++;
                    Console.WriteLine("Car successfully removed.");
                    return true;
                }
            }
            Console.WriteLine("Can't remove car.\nThis usually happens when the car does not exist or the vehicle's balance is too low." +
                "\nCheck request and try again.");
            return false;
        }

        public static void PrintTransactions()
        {
            if (Transactions == null || Transactions.Count == 0)
            {
                Console.WriteLine("No transactions yet...");
                return;
            }
            foreach (Transaction tr in Transactions.ToArray())
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Current time: {0}", DateTime.Now);
                Console.WriteLine("Transaction time: {0}", tr.DateTimeTransaction);
                Console.WriteLine("Car id: {0}", tr.CarId);
                Console.WriteLine("Written-off funds: {0}", tr.WrittenOff_Funds);
                Console.WriteLine("------------------------------");
            }
        }
    }
}
