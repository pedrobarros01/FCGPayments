using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces;
using FCG.Payments.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Services;

public class SelectorStatus : ISelectorStatus
{
    private readonly IPaymentTransactionStatusRepository _paymentStatusRepository;

    public SelectorStatus(IPaymentTransactionStatusRepository paymentStatusRepository)
    {
        _paymentStatusRepository = paymentStatusRepository;
    }

    public async Task<Guid> GetRandomTransactionStatus()
    {
        IList<PaymentTransactionStatus> paymentTransactionsStatus = await _paymentStatusRepository.GetAll();
        Random random = new Random();
        int index = random.Next(0, 2);
        return paymentTransactionsStatus[index].Id;
    }
}
