using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoserviceLibrary.Entities;

namespace AutoserviceLibrary
{
    class AutoserviceCore
    {
        private Queue<PracovnikSkupiny1> _volnyPracovnici1;
        private Queue<PracovnikSkupiny2> _volnyPracovnici2;

        private Queue<Auto> _pokazeneAuto;
        private Queue<Auto> _opraveneAuto;

        private Queue<Zakaznik> _cakajuciZakaznik; 

        private AutoserviceGenerators Generatory;

        
    }
}
