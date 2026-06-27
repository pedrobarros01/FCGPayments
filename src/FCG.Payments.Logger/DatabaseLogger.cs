using FCG.Payments.Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace FCG.Payments.Logger;

public class DatabaseLogger : ILogger
{
    private readonly Channel<Log> _channel;
    private readonly string _category;

    public DatabaseLogger(Channel<Log> channel, string category)
    {
        _channel = channel;
        _category = category;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        Console.WriteLine(logLevel);
        if(logLevel == LogLevel.Error)
            _channel.Writer.TryWrite(new Log(logLevel.ToString(), _category, formatter(state, exception), exception?.ToString()));
        else
        {
            Console.WriteLine(formatter(state, exception));
        }
    }
}
