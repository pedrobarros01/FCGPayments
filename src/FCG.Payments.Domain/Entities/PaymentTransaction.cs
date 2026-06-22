using FCG.Payments.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Entities
{
    public class PaymentTransaction : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTransaction { get; set; }
        public PaymentTransactionStatus StatusTransaction { get; set; }

        public PaymentTransaction()
        {
            
        }

        

        public void Create(PaymentTransaction paymentTransaction, PaymentTransactionStatus statusTransaction)
        {
            base.CreateBaseEntity();
            UserId = paymentTransaction.UserId;
            GameId = paymentTransaction.GameId;
            Price = paymentTransaction.Price;
            StatusTransaction = statusTransaction;
            DateTransaction = DateTime.Now;
        }
    }
}
