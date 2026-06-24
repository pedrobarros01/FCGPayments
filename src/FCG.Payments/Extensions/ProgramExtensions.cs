using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Interfaces;
using FCG.Payments.Application.Services;
using FCG.Payments.Domain.Entities;
using FCG.Payments.Domain.Interfaces;
using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Domain.Services;
using FCG.Payments.Infrastructure.Data;
using FCG.Payments.Infrastructure.Messaging.Consumer;
using FCG.Payments.Infrastructure.Messaging.Producer;
using FCG.Payments.Infrastructure.Persistence;
using FCG.Payments.Infrastructure.Queue;
using FCG.Payments.Infrastructure.Repositories;
using FCG.Payments.Infrastructure.Services;
using FCG.Payments.Infrastructure.Settings;
using FCG.Payments.Logger;

//using FCG.Payments.Logger;
using FCG.Shared.Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace FCG.Payments.Extensions
{
    public static class ProgramExtensions
    {

        public static IServiceCollection ConfigureWorker(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();
            services.AddHostedService<LoggerWorker>();

            return services;
        }
        public static IServiceCollection ConfigureDomain(this IServiceCollection services)
        {
            services.AddScoped<IPaymentTransactionDomainService, PaymentTransactionDomainService>();
            return services;
        }
        public static IServiceCollection ConfigureApplication(this IServiceCollection services)
        {
            services.AddSingleton<IProcessingQueue<TransactionCreate>, ProcessingQueue<TransactionCreate>>();

            services.AddScoped<IPaymentTransactionService, PaymentTransactionService>();
            services.AddScoped<ISelectorStatus, SelectorStatus>();
            return services;
        }
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSettings = configuration.GetSection("RabbitMQ").Get<RabbitMqSettings>();
            if (rabbitMqSettings == null ||
            string.IsNullOrWhiteSpace(rabbitMqSettings.Host) ||
            string.IsNullOrWhiteSpace(rabbitMqSettings.Username) ||
            string.IsNullOrWhiteSpace(rabbitMqSettings.Password) ||
            string.IsNullOrWhiteSpace(rabbitMqSettings.KeyQueueOrderPlaced))
            {
                throw new InvalidOperationException("RabbitMQ não configurado, verifique as ENVs do projeto");
            }
            var databaseConnection = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();
            if (databaseConnection == null ||
            string.IsNullOrWhiteSpace(databaseConnection.DefaultConnection))
            {
                throw new InvalidOperationException("String de conexão com o bancode não configurada, verifique as ENVs do projeto");
            }
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(databaseConnection.DefaultConnection));

            services.AddMassTransit(
                x =>
                {
                    x.AddConsumer<OrderPlacedConsumer>();
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.Host(
                           host: rabbitMqSettings.Host,
                           virtualHost: rabbitMqSettings.VirtualHost ?? "/",
                           h =>
                           {
                               h.Username(rabbitMqSettings.Username);
                               h.Password(rabbitMqSettings.Password);
                           }
                        );
                        cfg.ReceiveEndpoint(rabbitMqSettings.KeyQueueOrderPlaced, e =>
                        {
                            e.ConfigureConsumer<OrderPlacedConsumer>(context);
                        });

                        cfg.Publish<PaymentProcessedEvent>(p => p.ExchangeType = "topic");
                        cfg.ConfigureEndpoints(context);

                    });
                }

            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentProcessedPublisher, PaymentProcessedPublisher>();
            services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
            services.AddScoped<IPaymentTransactionStatusRepository, PaymentTransactionStatusRepository>();
            services.AddSingleton(Channel.CreateUnbounded<Log>());
            services.AddSingleton<ILoggerProvider, DatabaseLoggerProvider>();
            return services;
        }
    }
}
