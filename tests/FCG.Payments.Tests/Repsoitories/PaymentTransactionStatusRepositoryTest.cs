using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Enums;
using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Domain.Services;
using FCG.Payments.Infrastructure.Data;
using FCG.Payments.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Tests.Repsoitories;

public class PaymentTransactionStatusRepositoryTest
{
    [Fact]
    public async Task PaymentTransactionRepsoitory_Should_GetByIdOneStatus()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        await using var context = new ApplicationDbContext(options);

        var status = new PaymentTransactionStatus(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            "Aprovado",
            "Pagamento aprovado"
        );

        context.PaymentTransactionStatus.Add(status);
        await context.SaveChangesAsync();

        var repository = new PaymentTransactionStatusRepository(context);

        // Act
        var result = await repository.GetById(StatusOptions.Approved);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaymentTransactionStatus>(result);
        Assert.Equal(StatusOptions.Approved, result.Id);
    }
}
