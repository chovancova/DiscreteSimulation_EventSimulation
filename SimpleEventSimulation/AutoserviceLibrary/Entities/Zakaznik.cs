using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoserviceLibrary.Entities
{
    public class Zakaznik
    {
        private double _arrivalTimeToSystem;

        public bool JeAutoOpravene { get; set; }
        public bool JeAutoVOprave { get; set; }
        public int PocetOprav { get; set; }
        public int[] DobaOpravy { get; set; }
        public int CelkovaDobaOpravy { get; set; }

        public Zakaznik()
        {
            _arrivalTimeToSystem = -1;
        }

        //Statistics

        public void ZacniCakat(double time)
        {
            _arrivalTimeToSystem = time;
        }

        public double SkonciCakanie(double time)
        {
            if (_arrivalTimeToSystem < 0) return 0;
            return time - _arrivalTimeToSystem;
        }


        public void ZacniCakatAuto(double eventTime)
        {
            throw new NotImplementedException();
        }
    }
}
