using FCG.Payments.Domain.Entities;
using FCG.Payments.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Channels;

namespace FCG.Payments.Logger;

public class LoggerWorker : BackgroundService
{
    private readonly Channel<Log> _channel;
    private readonly IServiceScopeFactory _factory;

    public LoggerWorker(Channel<Log> channel, IServiceScopeFactory factory)
    {
        _channel = channel;
        _factory = factory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach(var log in _channel.Reader.ReadAllAsync(stoppingToken)) {
            using var scope = _factory.CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            if (context == null) throw new Exception("Contexto não encontrado");
            context.Logs.Add(log);
            await context.SaveChangesAsync();
        
        
        }
    }
}
