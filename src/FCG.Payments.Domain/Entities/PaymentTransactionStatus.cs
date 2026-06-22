using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Entities
{
    public class PaymentTransactionStatus : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<PaymentTransaction> Transactions { get; set; }
        public PaymentTransactionStatus()
        {
            
        }
        public PaymentTransactionStatus(Guid id)
        {
            Id = id;
        }
        public PaymentTransactionStatus(string name)
        {
            
            Name = name;
            Description = null;
        }
        public PaymentTransactionStatus(Guid id, string name, string description) : this(name)
        {
            Id = id;
            Description = description;
        }
    }
}
