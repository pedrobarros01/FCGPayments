using FCG.Payments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Interfaces;

public interface IPaymentProcessedPublisher
{
    Task PublishPaymentProcessed(PaymentTransaction transaction);
}
