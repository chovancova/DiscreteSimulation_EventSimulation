using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoserviceLibrary.Entities
{
    class Zakaznik
    {
        private double _arrivalTimeToSystem;

        public Zakaznik()
        {
            _arrivalTimeToSystem = -1;
        }

        //Statistics

        public void StartWaiting(double time)
        {
            _arrivalTimeToSystem = time;
        }

        public double EndWaiting(double time)
        {
            if (_arrivalTimeToSystem < 0) return 0;
            return time - _arrivalTimeToSystem;
        }

    }
}
