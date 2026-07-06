using FCG.Payments.Application.DTO;
using FCG.Payments.Infrastructure.Messaging.Consumer;
using FCG.Payments.Infrastructure.Queue;
using FCG.Shared.Events;
using MassTransit;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Tests.Messaging;

public class ConsumerTest
{
    [Fact]
    public void Consumer_Should_InsertItemInQueue()
    {
        Random random = new Random();
        var channel = new ProcessingQueue<TransactionCreate>();
        var consumer = new OrderPlacedConsumer(channel);
        var message = new OrderPlacedEvent(
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            "teste",
            (decimal)Random.Shared.NextDouble(),
            DateTime.UtcNow
        );
        var contextConsumer = new Mock<ConsumeContext<OrderPlacedEvent>>();
        contextConsumer
        .Setup(x => x.Message)
        .Returns(message);
        consumer.Consume(contextConsumer.Object);
        Assert.Equal(1, channel.CountItems());

    }
}
