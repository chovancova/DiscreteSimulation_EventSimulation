using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEventSimulation.ShopSimulation
{
    public class Customer
    {
        private double _arrivalTimeToSystem;

        public Customer()
        {
            _arrivalTimeToSystem = -1; 
        }

        //Statistics
        #region Statistics

        public void StartWaiting(double time)
        {
            _arrivalTimeToSystem = time;
        }

        public double EndWaiting(double time)
        {
            if (_arrivalTimeToSystem < 0) return 0;
            return time - _arrivalTimeToSystem; 
        }

        #endregion

    }
}
