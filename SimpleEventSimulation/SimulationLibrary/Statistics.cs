using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationLibrary
{
   public class Statistics
    {
        public double TimeDelta { get; set; }
        public double MeanAvarage { get; set; }

        private double _someObject;

        public Statistics()
        {
            TimeDelta = 0;
            MeanAvarage = 0;
            _someObject = 0;
        }

        public void Increment(double time)
        {
            Mean(time);
            _someObject++;
        }

        public void Decrement(double time)
        {
            Mean(time);
            _someObject--;
        }

        private void Mean(double time)
        {
            MeanAvarage += (time - TimeDelta) * _someObject;
            TimeDelta = time;
        }
    }
}
