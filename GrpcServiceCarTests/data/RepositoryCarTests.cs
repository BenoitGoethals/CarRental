using System;
using FluentAssertions;
using GrpcServiceCar.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;
using Shared.model;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace GrpcServiceCarTests.data
{
    public class RepositoryCarTests
    {
        private ILogger<RepositoryCar> LogTest;

        public RepositoryCarTests()
        {
            ILoggerFactory loggerFactory = new LoggerFactory();
            //or
            //ILoggerFactory loggerFactory = new LoggerFactory().AddConsole();

            LogTest = loggerFactory.CreateLogger<RepositoryCar>();
        }

        [Fact()]
        public void RepositoryCarTest()
        {
            string dbName = Guid.NewGuid().ToString();
            DbContextOptions<CarDbContext> options = new DbContextOptionsBuilder<CarDbContext>()
                .UseInMemoryDatabase(databaseName: dbName).Options;

            RepositoryCar repositoryCar=new RepositoryCar(logger:LogTest,new CarDbContext(options));
            Car car=new Car()
            {
                InCirculationDate=DateTime.Now,
                LastMaintenace=DateTime.Now,
                NextMaintenace=DateTime.Now,
                Km=100,
                Operational=true,
                PlateNbr= "dfdsfdsf",
                Remarks="sdsdfdsfds",
                Type=TypeCar.Coupe

            };


           repositoryCar?.AddCar(car);

           var retCar=repositoryCar.GetCarByplate("dfdsfdsf");

           car.Should().NotBe(null);
         
        }

        [Fact()]
        public void GetCarByplateTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void GetCaryIdTest()
        {

            // Setup Options
           
        }

        [Fact()]
        public void GetAllCarTest()
        {
            Assert.True(false, "This test needs an implementation");
        }

        [Fact()]
        public void AddCarTest()
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