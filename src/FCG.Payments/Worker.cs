using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments;

public sealed class Worker(ILogger<Worker> logger, IProcessingQueue<TransactionCreate> channel) : BackgroundService
{
    private readonly IProcessingQueue<TransactionCreate> _channel = channel;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested) 
        {
            //logger.LogInformation("Online e rodando");
            if(_channel.CountItems() > 0)
            {
                var item = await _channel.DequeueAsync(stoppingToken);
                logger.LogInformation($"UserId: {item.UserId} - GameId: {item.GameId} - Price: {item.Price}");
            }

        }

    }
}
