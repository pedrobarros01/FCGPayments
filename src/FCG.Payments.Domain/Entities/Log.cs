using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Domain.Entities;

public class Log : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    public string Level { get; set; }
    public string Category { get; set; }
    public string Message { get; set; }
    public string? Exception { get; set; }

    public Log(string level, string category, string message, string? exception)
    {
        CreatedAt = DateTime.UtcNow;
        Level = level;
        Category = category;
        Message = message;
        Exception = exception;
        base.CreateBaseEntity();
    }
}
