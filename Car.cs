namespace Parking
{
    public class Car
    {
        private int id;
        private double balance;
        private CarTypes type;

        public int Id{ get { return id; } set { id = value; } }
        public double Balance { get { return balance; } set { balance = value; } }
        public  CarTypes Type{ get { return type; } set { type = value; } }
    }
}
