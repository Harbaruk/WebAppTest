using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebTestApp.DAL.Entities;

namespace WebTestApp.DAL.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("transactions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Amount);
            builder.Property(x => x.CurrencyCode).HasMaxLength(5);
            builder.Property(x => x.TransactionDate);
            builder.Property(x => x.Status);
        }
    }
}
