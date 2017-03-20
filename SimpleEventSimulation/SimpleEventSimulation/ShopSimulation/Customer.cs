using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventSimulation.ShopSimulation
{
    public class Customer 
    {
        public int IdCustomer { get; set; }
        public double ArrivalTimeToSystem { get; set; }

        //Statistics

        #region Statistics
        private int _count { get; set; }
        public void Increment()
        {
            _count++;
        }

        public void Decrement()
        {
            _count--;
        }

        public bool Empty()
        {
            return _count == 0; 
        }

        #endregion

    }
}
