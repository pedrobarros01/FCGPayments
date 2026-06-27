using FCG.Payments.Application.Mapper;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces;
using FCG.Payments.Infrastructure.Settings;
using MassTransit;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Messaging.Producer;

public class PaymentProcessedPublisher : IPaymentProcessedPublisher
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly FCGSettings _settings;
    public PaymentProcessedPublisher(IPublishEndpoint publishEndpoint, IOptions<FCGSettings> settings)
    {
        _publishEndpoint = publishEndpoint;
        _settings = settings.Value;
    }

    public async Task PublishPaymentProcessed(PaymentTransaction transaction)
    {
        var @event = MapperStatic.MapPaymentTransactionToPaymentProcessedEvent(transaction);
        await _publishEndpoint.Publish(@event, context =>
        {
            context.SetRoutingKey(_settings.RabbitMQ.KeyPublisher);
        });
    }
}
