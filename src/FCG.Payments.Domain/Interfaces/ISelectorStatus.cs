using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Interfaces;

public interface ISelectorStatus
{
    int GetRandomTransactionStatus();
}
