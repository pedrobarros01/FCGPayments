using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Shared.Events;

public record OrderPlacedEvent(
    Guid UserId,
    Guid GameId,
    decimal Price
);

