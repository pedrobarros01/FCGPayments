using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Enums;

public static class StatusOptions
{
    public static readonly Guid Approved = new("11111111-1111-1111-1111-111111111111");
    public static readonly Guid Reproved = new("22222222-2222-2222-2222-222222222222");
}
