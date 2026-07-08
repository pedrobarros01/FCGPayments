using FCG.Payments.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Entities
{
    public class PaymentTransaction : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOnOrder { get; set; }
        public DateTime DateTransaction { get; set; }
        public PaymentTransactionStatus StatusTransaction { get; set; }

        public PaymentTransaction()
        {
            
        }

        

        public void Create(PaymentTransaction paymentTransaction, int statusTransaction)
        {
            base.CreateBaseEntity();
            UserId = paymentTransaction.UserId;
            GameId = paymentTransaction.GameId;
            Price = paymentTransaction.Price;
            StatusTransaction = (PaymentTransactionStatus)statusTransaction;
            DateTransaction = DateTime.UtcNow;
        }
    }
}
