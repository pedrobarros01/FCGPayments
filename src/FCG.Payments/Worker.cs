using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments;

public sealed class Worker(
    ILogger<Worker> logger,
    IProcessingQueue<TransactionCreate> channel,
    IServiceScopeFactory scopeFactory) : BackgroundService
{
    private readonly IProcessingQueue<TransactionCreate> _channel = channel;
    private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                if (_channel.CountItems() > 0)
                {
                    var item = await _channel.DequeueAsync(stoppingToken);

                    using var scope = _scopeFactory.CreateScope();

                    var paymentTransactionService =
                        scope.ServiceProvider.GetRequiredService<IPaymentTransactionService>();

                    await paymentTransactionService.ProcessPayment(item);

                    logger.LogInformation(
                        "UserId: {UserId} - GameId: {GameId} - Price: {Price}",
                        item.UserId,
                        item.GameId,
                        item.Price);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar pagamento.");
            }
        }
    }
}