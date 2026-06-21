using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}
