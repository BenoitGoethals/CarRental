using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.model;

namespace GrpcServiceCar.data
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.Km).HasColumnName(nameof(Client.Email)).IsRequired();
            builder.Property(s => s.InCirculationDate).HasColumnName(nameof(Car.InCirculationDate)).IsRequired();
            builder.Property(s => s.LastMaintenace).HasColumnName(nameof(Car.LastMaintenace)).IsRequired();
            builder.Property(s => s.Operational).HasColumnName(nameof(Car.Operational)).IsRequired();
            builder.Property(s => s.NextMaintenace).HasColumnName(nameof(Car.NextMaintenace)).IsRequired();
            builder.Property(s => s.PlateNbr).HasColumnName(nameof(Car.PlateNbr)).IsRequired();
            builder.Property(s => s.Remarks).HasColumnName(nameof(Car.Remarks)).IsRequired();
            builder.Property(s => s.Type).HasColumnName(nameof(Car.Type)).IsRequired();


        }
    }
}
