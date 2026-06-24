using FCG.Payments.Application.Mapper;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Messaging.Producer;

public class PaymentProcessedPublisher : IPaymentProcessedPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;

    public PaymentProcessedPublisher(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishPaymentProcessed(PaymentTransaction transaction)
    {
        var @event = MapperStatic.MapPaymentTransactionToPaymentProcessedEvent(transaction);
        await _publishEndpoint.Publish(@event, context =>
        {
            context.SetRoutingKey("payment.processed");
        });
    }
}
