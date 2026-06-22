using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Services;

public class PaymentTransactionDomainService : IPaymentTransactionDomainService
{

    public Task<PaymentTransaction> CreatePaymentTransaction(PaymentTransaction transaction)
    {
        throw new NotImplementedException();
    }
}
