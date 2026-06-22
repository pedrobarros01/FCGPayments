using CommonTestUtilities.Database;
using CommonTestUtilities.Entities;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Enums;
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
        var status = new PaymentTransactionStatus(
            StatusOptions.Approved,
            "Aprovado",
            "Pagamento aprovado"
        );
        var paymentRepository = new Mock<IPaymentTransactionRepository>();
        var statusRepository = new Mock<IPaymentTransactionStatusRepository>();
        var selectorStatusService = new Mock<ISelectorStatus>();
        
        statusRepository
            .Setup(x => x.GetById(StatusOptions.Approved))
            .ReturnsAsync(status);

        statusRepository
            .Setup(x => x.GetAll())
            .ReturnsAsync([
            new PaymentTransactionStatus(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            "Aprovado",
            "Pagamento aprovado"
            ),
            new PaymentTransactionStatus(
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "Reprovado",
            "Pagamento reprovado"
            )
        ]);

        selectorStatusService
            .Setup(x => x.GetRandomTransactionStatus())
            .ReturnsAsync(StatusOptions.Approved);

        paymentRepository
            .Setup(x => x.Insert(It.IsAny<PaymentTransaction>()))
            .ReturnsAsync((PaymentTransaction p) => p);
        var paymentTransactionDomainService = new PaymentTransactionDomainService(paymentRepository.Object,
            statusRepository.Object, selectorStatusService.Object);
        var paymentTransaction = await paymentTransactionDomainService.CreatePaymentTransaction(paymentTransactionMock);
        List<Guid> statusIds = new List<Guid> { StatusOptions.Approved, StatusOptions.Reproved };
        Assert.NotNull(paymentTransaction);
        Assert.Contains(paymentTransaction.StatusTransaction.Id, statusIds);
    }
}
