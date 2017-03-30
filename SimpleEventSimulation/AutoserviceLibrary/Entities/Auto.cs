using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoserviceLibrary.Entities
{
    class Auto
    {
        public Auto(Zakaznik zakaznik)
        {
            Zakaznik = zakaznik;
        }

        public Zakaznik Zakaznik { get; set; }
    }
}
