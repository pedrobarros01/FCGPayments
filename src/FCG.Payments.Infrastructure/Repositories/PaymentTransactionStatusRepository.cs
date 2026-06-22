using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Repositories;

public class PaymentTransactionStatusRepository(ApplicationDbContext context) : BaseRepository<PaymentTransaction>(context), IPaymentTransactionStatusRepository
{
    public Task<PaymentTransactionStatus> GetById(Guid id)
    {
        throw new NotImplementedException();
    }
}
