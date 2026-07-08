using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Exceptions;
using FCG.Payments.Domain.Interfaces;
using FCG.Payments.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Services;

public class PaymentTransactionDomainService : IPaymentTransactionDomainService
{

    private readonly IPaymentTransactionRepository _paymentTransactionRepository;
    private readonly ISelectorStatus _selectorStatus;
    public PaymentTransactionDomainService(IPaymentTransactionRepository paymentTransactionRepository, ISelectorStatus selectorStatus)
    {
        _paymentTransactionRepository = paymentTransactionRepository;
        _selectorStatus = selectorStatus;
    }

    public async Task<PaymentTransaction> CreatePaymentTransaction(PaymentTransaction transaction)
    {
        int statusTransaction = _selectorStatus.GetRandomTransactionStatus();
        transaction.Create(transaction, statusTransaction);
        var insertedPaymentTransaction = await _paymentTransactionRepository.Insert(transaction);
        return insertedPaymentTransaction;

    }
}
