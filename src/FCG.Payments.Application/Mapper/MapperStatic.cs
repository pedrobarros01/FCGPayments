using FCG.Payments.Application.DTO;
using FCG.Payments.Domain.Entities;
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
            OrderId = @event.OrderId,
            CreatedOnOrder = @event.CreatedOn,
            GameName = @event.GameName,
            UserId = @event.UserId,
            Price = @event.Amount,
            GameId = @event.GameId,
        };
    }

    public static PaymentProcessedEvent MapPaymentTransactionToPaymentProcessedEvent(PaymentTransaction paymentTransaction)
    {
        int statusTransaction = paymentTransaction.StatusTransaction == Domain.Enum.PaymentTransactionStatus.Approved ? 1 : 2;
        return new PaymentProcessedEvent(
            paymentTransaction.OrderId,
            paymentTransaction.UserId,
            paymentTransaction.GameId,
            statusTransaction
        );
    }

}
