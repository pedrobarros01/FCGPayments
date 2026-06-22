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

        public PaymentTransaction(Guid userId, Guid gameId, decimal price)
        {
            base.CreateBaseEntity();
            UserId = userId;
            GameId = gameId;
            Price = price;
            DateTransaction = DateTime.Now;
        }

        public Guid GetRandomTransactionStatus()
        {
            Random random = new Random();
            int sortedNumber = random.Next(0, 10);
            if(sortedNumber <= 4)
            {
                return  StatusOptions.Approved;
            }
            else
            {
                return StatusOptions.Reproved;
            }
        }

        public void Create()
        {
            throw new NotImplementedException();
        }
    }
}
