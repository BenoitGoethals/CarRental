using Xunit;
using RentalBusLogic.logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.model;

namespace RentalBusLogic.logic.Tests
{
    public class ReservationManagerTests
    {
        [Fact()]
        public void GetReservvationTest()
        {
            ReservationManager reservationManager=new ReservationManager();
            Assert.NotNull(reservationManager.GetReservvation(DateTime.Now, DateTime.Now.AddDays(5), TypeCar.Coupe));
        }
    }
}