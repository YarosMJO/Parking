using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        static object locker = new object();
        static object locker1 = new object();

        private static int parkingSpace;
        private static double balance;
        private static double interimBalance = 0;

        private static List<Car> cars = null;
        private static List<Transaction> transactions = null;

        public static int ParkingSpace { get { return parkingSpace; } private set { parkingSpace = value; } }
        public static int ParkingSpaceLimit { get { return parkingSpace; } private set { parkingSpace = value; } }
        public static double Balance { get { return balance; } set { balance = value; } }
        public static double CurrentBalance { get { return interimBalance; } set { interimBalance = value; } }       

        public static List<Car> Cars { get { return cars; } private set { } }
        public static List<Transaction> Transactions { get { return transactions; } private set { } }


        static Parking()
        {
            Instance.InitParking();
        }

        private Parking() { }

        private void InitParking()
        {
            Settings.TimeOut = 3000;
            Settings.LogTime = 60000;
            ParkingSpace = Settings.ParkingSpace;
            ParkingSpaceLimit = ParkingSpace;
            cars = new List<Car>(ParkingSpace);
            transactions = new List<Transaction>();
        }

        public static void Count(object obj, EventArgs e)
        {
            lock (locker)
            {
                double money = 0;
                foreach (Car car in Cars.ToArray())
                {
                    foreach (KeyValuePair<string, int> entry in Settings.carPrices)
                    {
                        String sCarType = Convert.ToString(car.Type);
                        if (sCarType == entry.Key)
                        {
                            if (car.Balance < entry.Value)
                                money = Settings.Fine * Settings.carPrices[sCarType];
                            else money = entry.Value;

                            CurrentBalance += money;
                            Balance += money;
                            car.Balance -= money;

                            Transactions.Add(new Transaction(car.Id, DateTime.Now, money));

                        }
                    }
                }
            }
        }

        public static void LogWriter(object obj, EventArgs e)
        {
            lock (locker1)
            {
                DateTime currentTime = DateTime.Now;

                using (StreamWriter w = File.AppendText(Settings.LOGPATH))
                {
                    foreach (Transaction tr in Transactions.ToArray())
                    {
                        TimeSpan tsp = tr.DateTimeTransaction - currentTime;
                        if (tsp.TotalMinutes <= 1)
                        {
                            Balance += tr.WrittenOff_Funds;
                        }
                    }
                    w.WriteLine("Current time:{0} Transaction sum:{1}", DateTime.Now, Balance);
                    Transactions.Clear();
                    CurrentBalance = 0;
                }

            }

        }
        public static void LogReader()
        {
            lock (locker1)
            {
                if (!File.Exists(Settings.LOGPATH))
                {
                    Console.WriteLine("Sorry... transaction log file not created yet. ");
                    return;
                }
                using (StreamReader r = File.OpenText(Settings.LOGPATH))
                {
                    DumpLog(r);
                }
            }
        }

        public static void DumpLog(StreamReader r)
        {
            string line;
            while ((line = r.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }

        public static int OccupiedSpacesCount()
        {
            return Settings.parkingSpaceLimit - ParkingSpace;
        }

        public static bool AddCar(Car newCar)
        {
            if (Cars.Any(car => car.Id == newCar.Id))
            {
                Console.WriteLine("Such car already exists. Try another car(id).");
                return false;
            }
            else if (newCar != null && Settings.ParkingSpace > Cars.Count)
            {
                cars.Add(newCar);
                ParkingSpace--;
                Console.WriteLine("Car successfully added.");
                return true;
            }
            else Console.WriteLine("Can't add new car");
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
        public static ArrayList ReadTransactions()
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

            return arrText;
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
