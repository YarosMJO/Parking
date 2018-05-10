using System.Collections;

namespace Parking
{
   public class Car: IEnumerable
    {
        private int id;
        private double balance;
        private CarTypes type;

        public int Id{ get { return id; } set { id = value; } }
        public double Balance { get { return balance; } set { balance = value; } }
        public  CarTypes Type{ get { return type; } set { type = value; } }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return id;
        }
    }
}
