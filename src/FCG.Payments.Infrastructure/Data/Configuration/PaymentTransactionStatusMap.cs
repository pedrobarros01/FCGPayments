using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Data.Configuration;

public class PaymentTransactionStatusMap : IEntityTypeConfiguration<PaymentTransactionStatus>
{
    public void Configure(EntityTypeBuilder<PaymentTransactionStatus> builder)
    {
        builder.ToTable("PaymentTransactionStatus");
        builder.HasKey(x => x.Id);
        builder.HasData(
            new PaymentTransactionStatus(StatusOptions.Approved,"Aprovado", "Referente a status de pagamento ser aprovado"),
            new PaymentTransactionStatus(StatusOptions.Reproved, "Reprovado", "Referente a status de pagamento ser reprovado")
        );
    }
}
