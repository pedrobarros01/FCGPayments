using CommonTestUtilities.Database;
using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Services;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Enums;
using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Domain.Services;
using FCG.Payments.Infrastructure.Persistence;
using FCG.Payments.Infrastructure.Repositories;
using FCG.Payments.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Tests.Service;

public class PaymentTransactionServiceTest
{
    [Fact]
    public async Task PaymentTransactionService_Should_CreateTransactio()
    {
        // Arrange
        await using var context = await ContextBuilder.GenerateContext();


        var repositoryPaymentTransactionStatus = new PaymentTransactionStatusRepository(context);
        var repositoryPaymentTransaction = new PaymentTransactionRepository(context);
        var selectorStatusService = new SelectorStatus(repositoryPaymentTransactionStatus);
        var domainService = new PaymentTransactionDomainService(repositoryPaymentTransaction, repositoryPaymentTransactionStatus, selectorStatusService);
        var unitOfWork = new UnitOfWork(context);
        var service = new PaymentTransactionService(unitOfWork, domainService);
        Random random = new Random();
        var transactionDTO = new TransactionCreate
        {
            GameId = Guid.NewGuid(),
            Price = (decimal)random.NextDouble(),
            UserId = Guid.NewGuid()
        };

        // Act
        var result = await service.ProcessPayment(transactionDTO);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaymentTransaction>(result);
        Assert.True(result.StatusTransactionId != Guid.Empty);
    }
}
