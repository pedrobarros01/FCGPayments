using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Repositories;

public class PaymentTransactionStatusRepository(ApplicationDbContext context) : BaseRepository<PaymentTransactionStatus>(context), IPaymentTransactionStatusRepository
{
    public async Task<PaymentTransactionStatus?> GetById(Guid id)
    {
        return await BaseQuery()
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}
