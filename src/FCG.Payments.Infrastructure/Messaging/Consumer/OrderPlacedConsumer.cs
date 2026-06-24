using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Interfaces;
using FCG.Payments.Application.Mapper;
using FCG.Shared.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Messaging.Consumer;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    public readonly IProcessingQueue<TransactionCreate> _channel;
    public OrderPlacedConsumer(IProcessingQueue<TransactionCreate> channel)
    {
        _channel = channel;
    }

    public Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        _channel.Enqueue(MapperStatic.MapOrderPlacedEvent(context.Message));
        return Task.CompletedTask;
    }
}
