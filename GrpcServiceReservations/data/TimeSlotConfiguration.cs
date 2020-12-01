using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.model;

namespace GrpcServiceCar.data
{
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(s => s.StartSlot).HasColumnName(nameof(TimeSlot.StartSlot)).IsRequired();
            builder.Property(s => s.EndSlot).HasColumnName(nameof(TimeSlot.EndSlot)).IsRequired();

         //   builder.HasOne<Car>(s => s.Car)
          //      .WithMany(g => g.TimeSlots)
      //          .HasForeignKey(s => s.CarId)
   //       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
