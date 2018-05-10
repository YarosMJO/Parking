using System;
using System.Threading;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking parking = Parking.Instance;

            Car Truck = new Car
            {
                Type = CarTypes.Truck,
                Balance = 20,
                Id = 1
            };

            Car Motorcycle = new Car
            {
                Type = CarTypes.Motorcycle,
                Balance = 20,
                Id = 2
            };

            Parking.AddCar(Truck);
            Parking.AddCar(Motorcycle);

            //try with 3 cars
            Thread.Sleep(5000);

            Car Bus = new Car
            {
                Type = CarTypes.Bus,
                Balance = 1,
                Id = 3
            };
            Parking.AddCar(Bus);
            
            //try with 2 cars again
            Thread.Sleep(5000);
            //we can't becouse balance<payment
            Parking.RemoveCar(Bus);
            

            Console.WriteLine("Parking balance"+Parking.Balance);

            Thread.Sleep(5000);
            Parking.Cars[2].Balance = 100;
            //all is ok, we can remove car
            Parking.RemoveCar(Bus);

            Console.WriteLine("Parking space: "+Parking.ParkingSpace);
            
            Settings.LogReader();

            Console.WriteLine("Parking balance{0}: ", Parking.Balance);
            Console.WriteLine("Interim  balance{0}: ", Parking.InterimBalance);
            Thread.Sleep(35000);
            Parking.PrintTransactions();
            Console.ReadKey();
        }
    }
}
