using System;
using System.Collections.Generic;

namespace Parking
{
    public sealed class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(()=>new Parking());
      
        public static Parking Instance
        {
            get { return lazy.Value; }
        }

        private static int parkingSpace;
        private static double balance;
        private static List<Car> cars = null;
        private static List<Transaction> transactions;

        public static int ParkingSpace { get { return parkingSpace; } private set { parkingSpace = value; } }
        public static double Balance { get { return balance; } set { balance = value; } }

        public  static List<Car> Cars { get { return cars; } private set { } }
        public static List<Transaction> Transactions { get { return transactions; } private set { } }
        

        private Parking()
        {
            InitParking();
        }

        private void InitParking()
        {
            Settings.TimeOut = 3000;
            Settings.LogTime = 150000;
            parkingSpace = Settings.ParkingSpace;
            cars = new List<Car>();
            transactions = new List<Transaction>();
        }

        public static bool AddCar(Car car)
        {
            if (car != null && parkingSpace > cars.Count)
            {
                cars.Add(car);
                ParkingSpace--;
                return true;
            }
            return false;
        }

        public static bool RemoveCar(Car car)
        {
            
            foreach (Car el in cars)
            {
                if (el == car && car.Balance > Settings.carPrices[Convert.ToString(car.Type)])
                {
                    cars.Remove(el);
                    ParkingSpace++;
                    return true;
                }
            }
            return false;
        }
    }
}
