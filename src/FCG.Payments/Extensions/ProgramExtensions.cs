using FCG.Payments.Application.DTO;
using FCG.Payments.Application.Interfaces;
using FCG.Payments.Application.Services;
using FCG.Payments.Domain.Interfaces;
using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Domain.Services;
using FCG.Payments.Infrastructure.Data;
using FCG.Payments.Infrastructure.Messaging.Consumer;
using FCG.Payments.Infrastructure.Persistence;
using FCG.Payments.Infrastructure.Queue;
using FCG.Payments.Infrastructure.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Extensions
{
    public static class ProgramExtensions
    {

        public static IServiceCollection ConfigureWorker(this IServiceCollection services)
        {
            services.AddHostedService<Worker>();

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


                    });
                }    
                
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
