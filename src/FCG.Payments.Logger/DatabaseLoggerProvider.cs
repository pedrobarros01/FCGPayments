using FCG.Payments.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace FCG.Payments.Logger;

public class DatabaseLoggerProvider : ILoggerProvider
{
    private readonly Channel<Log> _channel;

    public DatabaseLoggerProvider(Channel<Log> channel)
    {
        _channel = channel;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new DatabaseLogger(_channel, categoryName);
    }

    public void Dispose()
    {
       
    }
}
