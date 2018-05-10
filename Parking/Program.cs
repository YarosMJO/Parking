using System;
using System.Threading;

namespace Parking
{
    class Program
    {
        static void Main(string[] args)
        {
            Parking parking = Parking.Instance;
            Car Truck = new Car();
            Truck.Type = CarTypes.Truck;
            Truck.Balance = 20;
            Car Motorcycle = new Car();
            Motorcycle.Type = CarTypes.Motorcycle;
            Motorcycle.Balance = 20;

            Parking.AddCar(Truck);
            Parking.AddCar(Motorcycle);

            //try with 3 cars
            Thread.Sleep(5000);

            Car Bus = new Car();
            Bus.Type = CarTypes.Bus;
            Bus.Balance = 1;
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
            Console.ReadKey();
        }
    }
}
