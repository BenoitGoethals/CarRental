using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.model;

namespace GrpcServiceCar.data
{
    public class CarDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<Car> Cars { get; set; }
        public CarDbContext(IConfiguration config, DbContextOptions<CarDbContext> options) : base(options)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //for (int i = 0; i < 20; i++)
            //{
            //    modelBuilder.Entity<Client>().HasData(new Client() { Id=-i,BirthDate = DateTime.Now, City = "Gent"+i, Country = "Belgie", DrivingLicence = "dsfdsf"+i, Email = "dfdsfd@dfds.be", ForName = "ben", IdCarNbr = "dsfds"+i, Name = "goet", Tel = "04785981552", Zip = "9200" });
            //}



        }

    }
}
