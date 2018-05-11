using System;

namespace Parking
{
    public static class Menu
    {
        public static void ShowMenu()
        {

            int id, sum = 0;
            string buf;
            bool check = false;

            while (true)
            {
                PrintMenuTemplate();
                buf = Console.ReadLine();
                Console.Clear();
                switch (buf)
                {
                    case "1":
                        Car car = new Car();
                        Console.WriteLine("Please enter car type(Passenger(0),Truck(1),Bus(2),Motorcycle(3)): ");
                        while (!check)
                        {
                            try
                            {
                                car.Type = (CarTypes)Enum.Parse(typeof(CarTypes), Console.ReadLine());
                                if (int.TryParse(Convert.ToString(car.Type), out int a))
                                    Console.WriteLine("Not valid car type. Car type can not contains number. Please try again.");
                                else check = true;
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Not valid car type. Please try again.");
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Not valid car type. Car type can not contains number. Please try again.");
                            }
                        }
                        check = false;
                        
                        Console.WriteLine("Please enter car id: ");
                        while (!Int32.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Enter valid id please: ");
                        };
                        car.Id = id;

                        Parking.AddCar(car);
                        break;
                    case "2":
                        Console.WriteLine("Please enter car id: ");
                        while (!Int32.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Enter valid id please: ");
                        };

                        Parking.RemoveCar(id);
                        break;
                    case "3":                        
                        Console.WriteLine("Please enter car id: ");
                        while (!Int32.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Enter valid id please: ");
                        };
                        Console.WriteLine("Input sum please: ");
                        while (!Int32.TryParse(Console.ReadLine(), out sum))
                        {
                            Console.WriteLine("Enter valid sum please: ");
                        };
                    
                        Car c = Parking.Cars.Find(cars => cars.Id == id);                           
                        if (c == null)
                        {
                          Console.WriteLine("No such car at parking");
                        }
                        else c.Balance += sum;
                        
                        break;
                    case "4":
                        Parking.PrintTransactions();
                        break;
                    case "5":
                        Settings.LogReader();
                        break;
                    case "6":
                        Console.WriteLine("Total parking balance:{0}",Parking.Balance);
                        break;
                    case "7":
                        Console.WriteLine("Current parking balance:{0}", Parking.CurrentBalance);
                        break;
                    case "8":
                        Console.WriteLine("Free parking spaces: {0}", Parking.ParkingSpace);
                        break;
                    case "9":
                        Console.WriteLine("Occupied parking spaces: {0}/{1}",Settings.parkingSpaceLimit - Parking.ParkingSpace,
                           Settings.parkingSpaceLimit);
                        break;
                    case "10":
                        Environment.Exit(0);
                        return;
                    default:
                        Console.WriteLine("Please enter only existing menu option numbers!");
                        break;
                }
            }
        }
        private static void PrintMenuTemplate()
        {
            Console.WriteLine("┌───────────────────────────────────┐");
            Console.WriteLine("│1)Add car                          │");
            Console.WriteLine("│2)Delete car                       │");
            Console.WriteLine("│3)Refill the car balance           │");
            Console.WriteLine("│4)Display last minute transactions │");
            Console.WriteLine("│5)Display Transaction.log file     │");
            Console.WriteLine("│6)Display total parking balance    │");
            Console.WriteLine("│7)Display current parking balance  │");
            Console.WriteLine("│8)Display free parking spaces      │");
            Console.WriteLine("│9)Display occupied parking spaces  │");
            Console.WriteLine("│10Exit                             │");
            Console.WriteLine("└───────────────────────────────────┘");
        }
    }
}
