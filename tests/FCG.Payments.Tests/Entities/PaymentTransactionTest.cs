
using CommonTestUtilities.Entities;
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

        transaction.CreateTransactionStatus();
        List<Guid> statusIds = new List<Guid> { StatusOptions.Approved, StatusOptions.Reproved};
        Assert.All(statusIds, s => Assert.Equal(s, transaction.StatusTransaction.Id));
        
    }
}
