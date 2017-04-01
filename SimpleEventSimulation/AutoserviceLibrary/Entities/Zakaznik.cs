using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoserviceLibrary.Entities
{
    public class Zakaznik
    {
        private double _prichodDoSystemu;
        private double _prichodDoSystemuPokazenych;
        private double _cakanieNaOpravu; 
        private double _cakanieNaVybavenieObjednavky; 

        public bool JeAutoOpravene { get; set; }
        public bool JeAutoVOprave { get; set; }
        public int PocetOprav { get; set; }
        public int[] DobaOpravy { get; set; }
        public int CelkovaDobaOpravy { get; set; }

        public Zakaznik()
        {
            _prichodDoSystemu = -1;
        }

        //Statistics
        public void VynulujStatistiky()
        {
            _prichodDoSystemu = -1;
        }

        public void ZacniCakatVRade(double time)
        {
            _prichodDoSystemu = time;
        }

        public double SkonciCakanieVRade(double time)
        {
            if (_prichodDoSystemu < 0) return 0;
            return time - _prichodDoSystemu;
        }
        //pri spracovani objednavky
        public void ZacniCakatNaVybavenieObjednavky(double time)
        {
            _cakanieNaVybavenieObjednavky = time;
        }

        public double SkonciCakanieNaVybavenieObjednavky(double time)
        {
            if (_cakanieNaVybavenieObjednavky < 0)
                return 0;
            return time - _cakanieNaVybavenieObjednavky;
        }
        
        public void ZacniCakatNaOpravu(double eventTime)
        {
            _cakanieNaOpravu = eventTime;
        }

        public double SkonciCakanieNaOpravu(double time)
        {
            if (_cakanieNaOpravu < 0)
                return 0;
            return time - _cakanieNaOpravu;
        }

        public void ZacniCakatVRadePokazenych(double time)
        {
            _prichodDoSystemuPokazenych = time;
        }

        public double SkonciCakanieVRadePokazenych(double time)
        {
            if (_prichodDoSystemuPokazenych < 0) return 0;
            return time - _prichodDoSystemuPokazenych;
        }

    }
}
