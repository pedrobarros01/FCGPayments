using FCG.Payments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Interfaces.Repositories
{
    public interface IPaymentTransactionStatusRepository : IBaseRepository<PaymentTransactionStatus>
    {
        Task<PaymentTransactionStatus?> GetById(Guid id);
    }
}
