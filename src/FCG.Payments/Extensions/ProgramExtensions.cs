using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Infrastructure.Data;
using FCG.Payments.Infrastructure.Persistence;
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
        public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
