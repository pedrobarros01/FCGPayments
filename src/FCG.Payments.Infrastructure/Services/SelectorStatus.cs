using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces;
using FCG.Payments.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Services;

public class SelectorStatus : ISelectorStatus
{

    public SelectorStatus()
    {
    }

    public int GetRandomTransactionStatus()
    {
        
        Random random = new Random();
        int index = random.Next(0, 2);
        return index;
    }
}
