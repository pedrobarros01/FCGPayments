using FCG.Payments.Domain.Entities;
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

            builder.HasOne(p => p.StatusTransaction)
                .WithMany(ps => ps.Transactions)
                .HasForeignKey(p => p.StatusTransactionId);
        }
    }
}
