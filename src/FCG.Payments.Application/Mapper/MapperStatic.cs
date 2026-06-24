using FCG.Payments.Application.DTO;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Enums;
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

    public static PaymentProcessedEvent MapPaymentTransactionToPaymentProcessedEvent(PaymentTransaction paymentTransaction)
    {
        string statusTransaction = paymentTransaction.StatusTransactionId == StatusOptions.Approved ? "APPROVED" : "REPROVED";
        return new PaymentProcessedEvent(
            paymentTransaction.UserId,
            paymentTransaction.GameId,
            statusTransaction
        );
    }

}
