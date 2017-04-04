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

        private double _zaciatok_cakania_front_cakajucich_zakaznikov;
        private double _zaciatok_cakania_bytia_v_servise;
        private double _zaciatok_cakania_na_opravu;

       public Zakaznik()
        {
            _prichodDoSystemu = -1;

            _zaciatok_cakania_front_cakajucich_zakaznikov = -1;
            _zaciatok_cakania_bytia_v_servise = -1;
            _zaciatok_cakania_na_opravu = -1; 
        }

        //Statistics
        public void VynulujStatistiky()
        {
            _prichodDoSystemu = -1;
            _zaciatok_cakania_front_cakajucich_zakaznikov = -1;
            _zaciatok_cakania_bytia_v_servise = -1;
            _zaciatok_cakania_na_opravu = -1;
        }

        public void S1_ZacniCakanie_front_cakajucich_zakaznikov(double time)
        {
            _zaciatok_cakania_front_cakajucich_zakaznikov = time;
        }

        public double S1_SkonciCakanie_front_cakajucich_zakaznikov(double time)
        {
            if (_zaciatok_cakania_front_cakajucich_zakaznikov < 0) return 0;
            return time - _zaciatok_cakania_front_cakajucich_zakaznikov;
        }

        public void S3_ZacniCakanie_bytia_v_servise(double time)
        {
            _zaciatok_cakania_bytia_v_servise = time;
            _zaciatok_cakania_front_cakajucich_zakaznikov = time;
        }

        public double S3_SkonciCakanie_bytia_v_servise(double time)
        {
            if (_zaciatok_cakania_bytia_v_servise < 0) return 0;
            return time - _zaciatok_cakania_bytia_v_servise;
        }

        public void S4_ZacniCakanie_oprava(double time)
        {
            _zaciatok_cakania_na_opravu = time;
        }

        public double S4_SkonciCakanie_oprava(double time)
        {
            if (_zaciatok_cakania_na_opravu < 0) return 0;
            return time - _zaciatok_cakania_na_opravu;
        }
        
        
    }
}
