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

    public PaymentTransactionService(IUnitOfWork unitOfWork, IPaymentTransactionDomainService paymentTransactionDomainService) : base(unitOfWork)
    {
        _paymentTransactionDomainService = paymentTransactionDomainService ?? throw new ArgumentNullException(nameof(paymentTransactionDomainService));
    }

    public async Task<PaymentTransaction> ProcessPayment(TransactionCreate transaction)
    {
        PaymentTransaction paymentTransaction = new PaymentTransaction
        {
            GameId = transaction.GameId,
            Price = transaction.Price,
            UserId = transaction.UserId
        };
        var transactionInserted = await _paymentTransactionDomainService.CreatePaymentTransaction(paymentTransaction);
        await UnitOfWork.CommitAsync();
        //Chamar função de publicar evento para notification e catalog
        return transactionInserted;
    }
}
