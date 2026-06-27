using FCG.Payments.Application.DTO;
using FCG.Payments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.Interfaces;

public interface IPaymentTransactionService
{
    Task<PaymentTransaction> ProcessPayment(TransactionCreate transaction);
}
