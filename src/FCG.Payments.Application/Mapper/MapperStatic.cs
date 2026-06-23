using FCG.Payments.Application.DTO;
using FCG.Shared.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.Mapper;

public static class MapperStatic
{
    public static TransactionCreate MapOrderPlacedEvent(OrderPlacedEvent @event)
    {
        return new TransactionCreate
        {
            UserId = @event.UserId,
            Price = @event.Price,
            GameId = @event.GameId,
        };
    }
}
