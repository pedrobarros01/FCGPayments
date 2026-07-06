using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Application.DTO
{
    public record TransactionCreate
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedOnOrder { get; set; }
    }
}
