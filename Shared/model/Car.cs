using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Shared.model
{
    public class Car
    {
        public int Id { get; set; }
        public string PlateNbr { get; set; }

        public TypeCar Type { get; set; }

        public int Km { get; set; }

        public bool Operational { get; set; }

        public DateTime LastMaintenace { get; set; }

        public DateTime NextMaintenace { get; set; }

        public DateTime InCirculationDate { get; set; }

        public string Remarks { get; set; }

        public List<TimeSlot> TimeSlots =new List<TimeSlot>();
      
    }

    public enum TypeCar
    {
        Unknown=0,
        Coupe = 1,
        Sedan = 2,
        MiniVan = 3
    }
}
