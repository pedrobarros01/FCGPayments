using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments;

public sealed class Worker(ILogger<Worker> logger, IProcessingQueue<TransactionCreate> channel, IPaymentTransactionService paymentTransactionService) : BackgroundService
{
    private readonly IProcessingQueue<TransactionCreate> _channel = channel;
    private readonly IPaymentTransactionService _paymentTransactionService = paymentTransactionService;
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_channel.CountItems() > 0)
                {
                    var item = await _channel.DequeueAsync(stoppingToken);
                    await _paymentTransactionService.ProcessPayment(item);
                    logger.LogInformation($"UserId: {item.UserId} - GameId: {item.GameId} - Price: {item.Price}");
                }
            }
            catch (Exception e)
            {

                logger.LogError(e.Message, e);
            }


        }

    }

}
