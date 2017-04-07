using System;

namespace AutoserviceLibrary.Entities
{
    
    public class Zakaznik
    {
        public double DobaCakanieBytiaVServise { get; private set; }
        public double DobaCakaniaFrontCakajucichZakaznikov { get; private set; }
        public double DobaCakaniaNaOpravu { get; private set; }
        public double DobaVSysteme { get; private set; }
        private double _zaciatokCakaniaBytiaVServise;
        private double _zaciatokCakaniaFrontCakajucichZakaznikov;
        private double _zaciatokCakaniaNaOpravu;

        public Zakaznik()
        {
            _zaciatokCakaniaFrontCakajucichZakaznikov = -1;
            _zaciatokCakaniaBytiaVServise = -1;
            _zaciatokCakaniaNaOpravu = -1;

            DobaVSysteme = -1;

            DobaCakaniaFrontCakajucichZakaznikov = -1;
            DobaCakanieBytiaVServise = -1;
            DobaCakaniaNaOpravu = -1;
        }

        //Statistics
        public void VynulujStatistiky()
        {
            _zaciatokCakaniaFrontCakajucichZakaznikov = -1;
            _zaciatokCakaniaBytiaVServise = -1;
            _zaciatokCakaniaNaOpravu = -1;
            DobaCakaniaFrontCakajucichZakaznikov = -1;
            DobaCakanieBytiaVServise = -1;
            DobaCakaniaNaOpravu = -1;
            DobaVSysteme = -1;

        }

        public void S1_ZacniCakanie_front_cakajucich_zakaznikov(double time)
        {
            _zaciatokCakaniaFrontCakajucichZakaznikov = time;
        }

        public double S1_SkonciCakanie_front_cakajucich_zakaznikov(double time)
        {
            if (_zaciatokCakaniaFrontCakajucichZakaznikov < 0) return 0;

            DobaCakaniaFrontCakajucichZakaznikov = time - _zaciatokCakaniaFrontCakajucichZakaznikov;
            return DobaCakaniaFrontCakajucichZakaznikov;
        }

        public void S3_ZacniCakanie_bytia_v_servise(double time)
        {
            _zaciatokCakaniaBytiaVServise = time;
        }

        public double S3_SkonciCakanie_bytia_v_servise(double time)
        {
            if (_zaciatokCakaniaBytiaVServise < 0) return 0;
            DobaCakanieBytiaVServise = time - _zaciatokCakaniaBytiaVServise;
            return DobaCakanieBytiaVServise;
        }

        public void S4_ZacniCakanie_oprava(double time)
        {
            _zaciatokCakaniaNaOpravu = time;
        }

        public double S4_SkonciCakanie_oprava(double time)
        {
        if (_zaciatokCakaniaNaOpravu < 0) return 0;
            DobaCakaniaNaOpravu = time - _zaciatokCakaniaNaOpravu;
            return DobaCakaniaNaOpravu;
        }

        public double S5_SkonciCakanie_system(double time)
        {
            if (_zaciatokCakaniaBytiaVServise < 0) throw new Exception("Zle nastavene. ");
            if (_zaciatokCakaniaNaOpravu < 0) throw new Exception("Zle nastavene. ");

            DobaVSysteme = DobaCakaniaNaOpravu + DobaCakaniaNaOpravu;
            return DobaVSysteme;
        }
    }
}