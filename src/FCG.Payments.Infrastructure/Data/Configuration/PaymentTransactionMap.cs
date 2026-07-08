using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Data.Configuration
{
    public class PaymentTransactionMap : IEntityTypeConfiguration<PaymentTransaction>
    {
        public void Configure(EntityTypeBuilder<PaymentTransaction> builder)
        {
            builder.ToTable("PaymentTransaction");
            builder.HasKey(x => x.Id);

            builder.Property(p => p.StatusTransaction)
            .IsRequired()
            .HasConversion(
                status => status.ToString(),
                value => Enum.Parse<PaymentTransactionStatus>(value))
            .HasMaxLength(30);
        }
    }
}
