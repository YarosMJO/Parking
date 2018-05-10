using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;

namespace Parking
{
    static class Settings
    {
        private static Timer aTimer,bTimer;

        private static readonly string LOGPATH = "D:/Transaction.log";
        private static int parkingSpace = 20;
        private static double fine = 0.5;

        public static int ParkingSpace { get { return parkingSpace; } private set { parkingSpace = value; } }
        public static double Fine { get { return fine; } set { fine = value; } }
        public static readonly Dictionary<string, int> carPrices 
            = new Dictionary<string, int>
            {   
                { "Truck", 5 },
                { "Passenger", 3},
                { "Bus",2},
                {"Motorcycle",1}
            };

        public static int TimeOut
        {
            set
            {
                aTimer = new Timer(value = 3000);
                aTimer.Elapsed += new ElapsedEventHandler(Count);
                aTimer.Enabled = true;

            }
        }
        public static int LogTime
        {
            set
            {
                bTimer = new Timer(value = 60000);
                bTimer.Elapsed += new ElapsedEventHandler(LogWriter);
                bTimer.Enabled = true;

            }
        }

        private static void Count(object obj,EventArgs e)
        {
            object locker = new object();
            lock (locker)
            {
                double money = 0;
                foreach (Car car in Parking.Cars)
                {
                    foreach (KeyValuePair<string, int> entry in carPrices)
                    {
                        String sCarType = Convert.ToString(car.Type);
                        if (sCarType == entry.Key)
                        {
                            if (car.Balance < entry.Value)
                                money = Fine * carPrices[sCarType];
                            else money = entry.Value;

                            Parking.Balance += money;
                            car.Balance -= money;

                            Parking.Transactions.Add(new Transaction(car.Id, DateTime.Now, money));

                            Console.WriteLine(car.Balance);
                        }

                    }
                }
            }
        }

    

        public static void LogWriter(object obj, EventArgs e)
        {
            object locker = new object();
            lock (locker)
            {
                double sum = 0;
                DateTime currentTime = DateTime.Now;

                using (StreamWriter w = File.AppendText(LOGPATH))
                {
                    foreach (Transaction tr in Parking.Transactions.ToArray())
                    {
                        TimeSpan tsp = tr.DateTimeTransaction - currentTime;
                        if (tsp.TotalMinutes <= 1) {
                            sum += tr.WrittenOff_Funds;
                        }
                    }
                    w.WriteLine("Current time:{0} Transaction sum:{1}", DateTime.Now, sum);

                }
            }
        }
        public static void LogReader()
        {
            object locker = new object();
            lock (locker)
            {
                if (!File.Exists(LOGPATH))
                {
                    Console.WriteLine("Sory... transaction log file not created yet. ");
                    return;
                }
                using (StreamReader r = File.OpenText(LOGPATH))
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

    }

}
