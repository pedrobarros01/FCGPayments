using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Interfaces;

public interface IBaseEntity
{
    Guid Id { get; set; }
}
