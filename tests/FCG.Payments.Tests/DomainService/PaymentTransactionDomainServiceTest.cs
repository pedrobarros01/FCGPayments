using CommonTestUtilities.Entities;
using FCG.Payments.Domain.Enums;
using FCG.Payments.Domain.Services;
using FCG.Payments.Tests.Fixture;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Tests.DomainService;

[Collection(nameof(PaymentTransactionFixtureCollection))]
public class PaymentTransactionDomainServiceTest
{
    public PaymentTransactionBuilder _transactionFixture;
    public PaymentTransactionDomainServiceTest(PaymentTransactionBuilder transactionFixture)
    {
        _transactionFixture = transactionFixture;
    }
    [Fact]
    public async Task PaymentTransactionDomainService_Should_CreatePaymentTransactionWithStatus()
    {
        var paymentTransactionMock = _transactionFixture.GenerateTransactionWithStatusEmpty();
        var paymentTransactionDomainService = new PaymentTransactionDomainService();
        var paymentTransaction = await paymentTransactionDomainService.CreatePaymentTransaction(paymentTransactionMock);
        List<Guid> statusIds = new List<Guid> { StatusOptions.Approved, StatusOptions.Reproved };
        Assert.NotNull(paymentTransaction);
        Assert.Contains(paymentTransaction.StatusTransaction.Id, statusIds);
    }
}
