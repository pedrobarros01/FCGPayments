using CommonTestUtilities.Database;
using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Mapper;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Infrastructure.Repositories;
using FCG.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Tests.Mapper
{
    public class MapperStaticTest
    {

        [Fact]
        public async Task MapperStatic_Should_MapOrderEventToTransactionCreate()
        {
            //Arrange
            Random random = new Random();
            var @event = new OrderPlacedEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), "teste", (decimal)random.NextDouble(), DateTime.UtcNow);
            TransactionCreate? mappedObject = MapperStatic.MapOrderPlacedEvent(@event);

            Assert.NotNull(mappedObject);
            Assert.Equal(@event.UserId, mappedObject.UserId);
            Assert.Equal(@event.GameId, mappedObject.GameId);
            Assert.Equal(@event.Amount, mappedObject.Price);
        }
        [Fact]
        public async Task MapperStatic_Should_MapPaymentTransactionToPaymentProcessedApprovedEvent()
        {
            //Arrange
            Random random = new Random();
            var @event = new PaymentTransaction
            {
                DateTransaction = DateTime.Now,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                Price = (decimal)random.NextDouble(),
                StatusTransaction = Domain.Enum.PaymentTransactionStatus.Approved,
                UserId = Guid.NewGuid()
            };
            PaymentProcessedEvent mappedObject = MapperStatic.MapPaymentTransactionToPaymentProcessedEvent(@event);

            Assert.NotNull(mappedObject);
            Assert.Equal(@event.UserId, mappedObject.UserId);
            Assert.Equal(@event.GameId, mappedObject.GameId);
            Assert.Equal("Approved", mappedObject.Status);
        }
        [Fact]
        public async Task MapperStatic_Should_MapPaymentTransactionToPaymentProcessedReprovedEvent()
        {
            //Arrange
            Random random = new Random();
            var @event = new PaymentTransaction
            {
                DateTransaction = DateTime.Now,
                GameId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                Price = (decimal)random.NextDouble(),
                StatusTransaction = Domain.Enum.PaymentTransactionStatus.Rejected,
                UserId = Guid.NewGuid()
            };
            PaymentProcessedEvent mappedObject = MapperStatic.MapPaymentTransactionToPaymentProcessedEvent(@event);

            Assert.NotNull(mappedObject);
            Assert.Equal(@event.UserId, mappedObject.UserId);
            Assert.Equal(@event.GameId, mappedObject.GameId);
            Assert.Equal("Rejected", mappedObject.Status);
        }
    }
}
