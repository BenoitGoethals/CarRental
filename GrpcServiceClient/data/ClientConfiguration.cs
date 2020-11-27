using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.model;

namespace GrpcServiceClient.data
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        { builder.HasKey(b => b.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();
            
            builder.Property(s => s.Email).HasColumnName(nameof(Client.Email)).IsRequired();
            builder.Property(s => s.BirthDate).HasColumnName(nameof(Client.BirthDate)).IsRequired();
            builder.Property(s => s.City).HasColumnName(nameof(Client.City)).IsRequired();
            builder.Property(s => s.Country).HasColumnName(nameof(Client.Country)).IsRequired();
            builder.Property(s => s.DrivingLicence).HasColumnName(nameof(Client.DrivingLicence)).IsRequired();
            builder.Property(s => s.ForName).HasColumnName(nameof(Client.ForName)).IsRequired();
            builder.Property(s => s.IdCarNbr).HasColumnName(nameof(Client.IdCarNbr)).IsRequired();
            builder.Property(s => s.Name).HasColumnName(nameof(Client.Name)).IsRequired();
            builder.Property(s => s.Tel).HasColumnName(nameof(Client.Tel)).IsRequired();
            builder.Property(s => s.Zip).HasColumnName(nameof(Client.Zip)).IsRequired();
        }
    }
}
