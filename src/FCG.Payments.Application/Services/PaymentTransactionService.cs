using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Interfaces;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces;
using FCG.Payments.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.Services;

public class PaymentTransactionService : BaseApplicationService, IPaymentTransactionService
{
    private readonly IPaymentTransactionDomainService _paymentTransactionDomainService;
    private readonly IPaymentProcessedPublisher _paymentProcessedPublisher;

    public PaymentTransactionService(IUnitOfWork unitOfWork, IPaymentTransactionDomainService paymentTransactionDomainService, IPaymentProcessedPublisher paymentProcessedPublisher) : base(unitOfWork)
    {
        _paymentTransactionDomainService = paymentTransactionDomainService ?? throw new ArgumentNullException(nameof(paymentTransactionDomainService));
        _paymentProcessedPublisher = paymentProcessedPublisher;
    }

    public async Task<PaymentTransaction> ProcessPayment(TransactionCreate transaction)
    {
        PaymentTransaction paymentTransaction = new PaymentTransaction
        {
            GameName = transaction.GameName,
            OrderId = transaction.OrderId,
            GameId = transaction.GameId,
            Price = transaction.Price,
            UserId = transaction.UserId,
            CreatedOnOrder = transaction.CreatedOnOrder
        };
        var transactionInserted = await _paymentTransactionDomainService.CreatePaymentTransaction(paymentTransaction);
        await UnitOfWork.CommitAsync();
        await _paymentProcessedPublisher.PublishPaymentProcessed(transactionInserted);
        return transactionInserted;
    }
}
