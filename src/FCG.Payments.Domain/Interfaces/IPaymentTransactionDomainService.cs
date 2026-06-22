using FCG.Payments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Interfaces;

public interface IPaymentTransactionDomainService
{
    Task<PaymentTransaction> CreatePaymentTransaction(PaymentTransaction transaction);
}
