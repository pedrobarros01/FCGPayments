using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Interfaces;

public interface ISelectorStatus
{
    Task<Guid> GetRandomTransactionStatus();
}
