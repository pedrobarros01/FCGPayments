using CommonTestUtilities.Database;
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

namespace FCG.Payments.Tests.Repositories;

public class PaymentTransactionStatusRepositoryTest
{
    [Fact]
    public async Task PaymentTransactionStatusRepsoitory_Should_GetByIdOneStatus()
    {
        // Arrange
        var context = await ContextBuilder.GenerateContext();

        var repository = new PaymentTransactionStatusRepository(context);

        // Act
        var result = await repository.GetById(StatusOptions.Approved);

        // Assert
        Assert.NotNull(result);
        Assert.IsType<PaymentTransactionStatus>(result);
        Assert.Equal(StatusOptions.Approved, result.Id);
    }
    [Fact]
    public async Task PaymentTransactionStatusRepsoitory_Should_GetAll()
    {
        // Arrange
        var context = await ContextBuilder.GenerateContext();

        var repository = new PaymentTransactionStatusRepository(context);

        // Act
        var result = await repository.GetAll();

        // Assert
        Assert.True(result.Count > 0);
    }
}
