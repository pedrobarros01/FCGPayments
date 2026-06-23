using CommonTestUtilities.Database;
using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Mapper;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Enums;
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
            var @event = new OrderPlacedEvent(Guid.NewGuid(), Guid.NewGuid(), (decimal)random.NextDouble());
            TransactionCreate? mappedObject = MapperStatic.MapOrderPlacedEvent(@event);

            Assert.NotNull(mappedObject);
            Assert.Equal(@event.UserId, mappedObject.UserId);
            Assert.Equal(@event.GameId, mappedObject.GameId);
            Assert.Equal(@event.Price, mappedObject.Price);
        }
    }
}
