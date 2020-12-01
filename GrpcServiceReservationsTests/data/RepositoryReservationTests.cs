using System;
using FluentAssertions;
using GrpcServiceReservations.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared.model;
using Xunit;

namespace GrpcServiceReservationsTests.data
{
    public class RepositoryReservationTests
    {

        private ILogger<RepositoryReservation> LogTest;
        public RepositoryReservationTests()
        {
         //   ILoggerFactory loggerFactory = new LoggerFactory();
            //or
            ILoggerFactory loggerFactory = new LoggerFactory();
            

            LogTest = loggerFactory.CreateLogger<RepositoryReservation>();
            
        }

        [Fact()]
        public void RepositoryReservationTest()
        {
            string dbName = Guid.NewGuid().ToString();
            DbContextOptions<ReservationDbContext> options = new DbContextOptionsBuilder<ReservationDbContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options;

            RepositoryReservation repositoryCar = new RepositoryReservation(logger: LogTest, new ReservationDbContext(options));

            _ = repositoryCar.AddTimeSlot(new TimeSlot() { StartSlot = DateTime.Now, EndSlot = DateTime.Now.AddDays(1), CarId = 1 });
            //   TimeSlot timeSlot= repositoryCar.GetTimeSlot(1).Result;
            using var listofAllTimeSlots = repositoryCar.AllTimeSlots(1);
            listofAllTimeSlots.Result.Count.Should().BeGreaterThan(0);
        }

        [Fact()]
        public void RemoveTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddTimeSlotTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddTimeSlotByNameTest()
        {
            Assert.True(false, "This test needs an implementation");
        }
    }
}