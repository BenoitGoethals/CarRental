using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared.model;

namespace GrpcServiceClient.data
{
    public class ClientDbContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<Client> Clients { get; set; }
        public ClientDbContext(IConfiguration config, DbContextOptions<ClientDbContext> options) : base(options)
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
