using FCG.Payments.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public Guid Id { get; set; }
        public void CreateBaseEntity()
        {
            Id = Guid.NewGuid();
        }
    }
}
