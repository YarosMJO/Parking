using System.Collections.Generic;
using System.Timers;

namespace Parking
{
    static class Settings
    {
        private static Timer aTimer,bTimer;

        public static readonly int parkingSpaceLimit = 20;
        private static int parkingSpace = parkingSpaceLimit;
        public static readonly string LOGPATH = "Transaction.log";
        private static double fine = 1.2;

        public static int ParkingSpace { get { return parkingSpace; } private set { parkingSpace = value; } }       
        public static double Fine { get { return fine; } private set { fine = value; } }
        
        public static readonly Dictionary<string, int> carPrices 
            = new Dictionary<string, int>
            {   
                { "Truck", 5 },
                { "Passenger", 3},
                { "Bus", 2},
                { "Motorcycle", 1}
            };

        public static int TimeOut
        {
            set
            {
                aTimer = new Timer(3000);
                aTimer.Elapsed += new ElapsedEventHandler(Parking.Count);
                aTimer.Enabled = true;
            }
        }

        public static int LogTime
        {
            set
            {
                bTimer = new Timer(60000);
                bTimer.Elapsed += new ElapsedEventHandler(Parking.LogWriter);
                bTimer.Enabled = true;
            }
        }
    
    }
}
