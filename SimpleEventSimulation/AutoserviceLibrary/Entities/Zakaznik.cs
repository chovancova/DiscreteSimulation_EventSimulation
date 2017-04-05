using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoserviceLibrary.Entities
{
    public enum TypZakaznika
    {
        Novy, 
        Objednavka, 
        PokazeneAuto, 
        OpraveneAuto, 
        Hotovy
    }
    public class Zakaznik
    {
        private double _zaciatokCakaniaFrontCakajucichZakaznikov;
        private double _zaciatokCakaniaBytiaVServise;
        private double _zaciatokCakaniaNaOpravu;

        private double _rozdielCakaniaFrontCakajucichZakaznikov;
        private double _rozdielCakaniaBytiaVServise;
        private double _rozdielCakaniaNaOpravu;

        public TypZakaznika Typ { get; set;  }


       public Zakaznik()
        {
            _zaciatokCakaniaFrontCakajucichZakaznikov = -1;
            _zaciatokCakaniaBytiaVServise = -1;
            _zaciatokCakaniaNaOpravu = -1;

            _rozdielCakaniaFrontCakajucichZakaznikov = -1;
            _rozdielCakaniaBytiaVServise = -1;
            _rozdielCakaniaNaOpravu = -1;
            Typ = TypZakaznika.Novy;
        }

        //Statistics
        public void VynulujStatistiky()
        {
            _zaciatokCakaniaFrontCakajucichZakaznikov = -1;
            _zaciatokCakaniaBytiaVServise = -1;
            _zaciatokCakaniaNaOpravu = -1;
            _rozdielCakaniaFrontCakajucichZakaznikov = -1;
            _rozdielCakaniaBytiaVServise = -1;
            _rozdielCakaniaNaOpravu = -1;
        }

        public void S1_ZacniCakanie_front_cakajucich_zakaznikov(double time)
        {
            Typ = TypZakaznika.Objednavka;

            _zaciatokCakaniaFrontCakajucichZakaznikov = time;
        }

        public double S1_SkonciCakanie_front_cakajucich_zakaznikov(double time)
        {
            if (_zaciatokCakaniaFrontCakajucichZakaznikov < 0) return 0;

            _rozdielCakaniaFrontCakajucichZakaznikov = time - _zaciatokCakaniaFrontCakajucichZakaznikov;
            return _rozdielCakaniaFrontCakajucichZakaznikov;
        }

        public void S3_ZacniCakanie_bytia_v_servise(double time)
        {
            _zaciatokCakaniaBytiaVServise = time;
        }

        public double S3_SkonciCakanie_bytia_v_servise(double time)
        {
            Typ = TypZakaznika.Hotovy;

            if (_zaciatokCakaniaBytiaVServise < 0) return 0;
            _rozdielCakaniaBytiaVServise = time - _zaciatokCakaniaBytiaVServise;
            return _rozdielCakaniaBytiaVServise;
        }

        public void S4_ZacniCakanie_oprava(double time)
        {
            Typ = TypZakaznika.PokazeneAuto;

            _zaciatokCakaniaNaOpravu = time;
        }

        public double S4_SkonciCakanie_oprava(double time)
        {
            Typ = TypZakaznika.OpraveneAuto;

            if (_zaciatokCakaniaNaOpravu < 0) return 0;
            _rozdielCakaniaNaOpravu = time - _zaciatokCakaniaNaOpravu;
            return _rozdielCakaniaNaOpravu;
        }
    }
}
