
using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Infrastructure.Repositories;
using FCG.Payments.Infrastructure.Services;
using FCG.Payments.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Tests.Entities;

[Collection(nameof(PaymentTransactionFixtureCollection))]
public class PaymentTransactionTest
{
    public PaymentTransactionBuilder _transactionFixture;
    public PaymentTransactionTest(PaymentTransactionBuilder transactionFixture)
    {
        _transactionFixture = transactionFixture;
    }
    [Fact]
    public async Task PaymentTransaction_Should_CreateRandomStatusTransaction()
    {
        var transaction = _transactionFixture.GenerateTransactionWithStatusEmpty();
        var context = await ContextBuilder.GenerateContext();
        var selectorStatusService = new SelectorStatus();
        var statusId = selectorStatusService.GetRandomTransactionStatus();
        List<int> statusIds = new List<int> { 0, 1 };
        Assert.Contains(statusId, statusIds);
        
    }

    [Fact]
    public void PaymentTransaction_Should_CreateObject()
    {
        var transaction = _transactionFixture.GenerateTransactionEmpty();
        var selectorStatusService = new SelectorStatus();
        var statusId = selectorStatusService.GetRandomTransactionStatus();
        transaction.Create(transaction, statusId);
        Assert.NotEqual(Guid.Empty, transaction.Id);

    }
}
