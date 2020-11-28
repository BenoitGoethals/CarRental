using System;
using System.Collections.Generic;
using System.Text;
using RentalBusLogic.model;
using Shared.model;

namespace RentalBusLogic.logic
{
    public class ReservationManager
    {
       private readonly List<Reservvation> _reservvations=new List<Reservvation>();
        public Reservvation GetReservvation(DateTime startReservation, DateTime endReservation, TypeCar typecar)
        {
          
            if (IsRevationPossible(startReservation, endReservation))
            {

                var reservation = new Reservvation();

                _reservvations.Add(reservation);

                return reservation;
            }

            return null;
        }

        private bool IsRevationPossible(in DateTime startReservation, in DateTime endReservation)
        {
            return true;
        }


        


        private Car GetCarForReservation(TypeCar typeCar)
        {
            return new Car();
        }
    }
}
