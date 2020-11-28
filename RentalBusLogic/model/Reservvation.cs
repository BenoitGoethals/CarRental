using System;
using Shared.model;

namespace RentalBusLogic.model
{
    public class Reservvation
    {
        public int Id { get; set; }
        public DateTime StartReservations { get; set; }

        public DateTime EndResvations { get; set; }

        public Car DesginatedCar { get; set; }

        public string Remarks { get; set; }
    }
}