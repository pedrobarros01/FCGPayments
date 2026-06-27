using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Shared.Events;

public record PaymentProcessedEvent(
    Guid UserId,
    Guid GameId,
    string StatusTransaction
);
