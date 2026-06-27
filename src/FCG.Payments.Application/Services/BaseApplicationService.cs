using FCG.Payments.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.Services
{
    public class BaseApplicationService(IUnitOfWork unitOfWork)
    {
        protected IUnitOfWork UnitOfWork { get; } = unitOfWork;
    }
}
