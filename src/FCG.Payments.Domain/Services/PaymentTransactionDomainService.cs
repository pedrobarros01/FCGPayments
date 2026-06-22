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
    private readonly IPaymentTransactionStatusRepository _paymentTransactionStatusRepository;

    public PaymentTransactionDomainService(IPaymentTransactionRepository paymentTransactionRepository, IPaymentTransactionStatusRepository paymentTransactionStatusRepository)
    {
        _paymentTransactionRepository = paymentTransactionRepository;
        _paymentTransactionStatusRepository = paymentTransactionStatusRepository;
    }

    public async Task<PaymentTransaction> CreatePaymentTransaction(PaymentTransaction transaction)
    {
        Guid statusRandomId = transaction.GetRandomTransactionStatus();
        PaymentTransactionStatus? statusTransaction = await _paymentTransactionStatusRepository.GetById(statusRandomId);
        if (statusTransaction == null) throw new BusinessException("Status da transação não encontrado");
        transaction.Create(transaction, statusTransaction);
        var insertedPaymentTransaction = await _paymentTransactionRepository.Insert(transaction);
        return insertedPaymentTransaction;

    }
}
