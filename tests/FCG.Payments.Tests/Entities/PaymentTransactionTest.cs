
using CommonTestUtilities.Entities;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Enums;
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
    public void PaymentTransaction_Should_CreateRandomStatusTransaction()
    {
        var transaction = _transactionFixture.GenerateTransactionWithStatusEmpty();

        Guid statusId = transaction.GetRandomTransactionStatus();
        List<Guid> statusIds = new List<Guid> { StatusOptions.Approved, StatusOptions.Reproved};
        Assert.Contains(statusId, statusIds);
        
    }

    [Fact]
    public void PaymentTransaction_Should_CreateObject()
    {
        var transaction = _transactionFixture.GenerateTransactionEmpty();
        var status = new PaymentTransactionStatus(Guid.NewGuid(), "teste", "teste");
        transaction.Create(transaction, status);
        Assert.NotEqual(Guid.Empty, transaction.Id);

    }
}
