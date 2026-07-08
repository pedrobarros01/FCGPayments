using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces;
using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Domain.Services;
using FCG.Payments.Infrastructure.Repositories;
using FCG.Payments.Infrastructure.Services;
using FCG.Payments.Tests.Fixture;
using Moq;
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

        var paymentRepository = new Mock<IPaymentTransactionRepository>();
        var selectorStatusService = new SelectorStatus();
        

        paymentRepository
            .Setup(x => x.Insert(It.IsAny<PaymentTransaction>()))
            .ReturnsAsync((PaymentTransaction p) => p);
        var paymentTransactionDomainService = new PaymentTransactionDomainService(paymentRepository.Object, selectorStatusService);
        var paymentTransaction = await paymentTransactionDomainService.CreatePaymentTransaction(paymentTransactionMock);
        List<int> statusIds = new List<int> { 0, 1 };
        Assert.NotNull(paymentTransaction);
        Assert.Contains((int)paymentTransaction.StatusTransaction, statusIds);
    }
}
