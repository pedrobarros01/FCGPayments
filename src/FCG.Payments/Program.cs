

using FCG.Payments;
using FCG.Payments.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services
    .ConfigureSettings(builder.Configuration)
    .ConfigureWorker()
    .ConfigureApplication()
    .ConfigureDomain()
    .ConfigureInfrastructure(builder.Configuration);

IHost host = builder.Build();
host.ApplyMigrations();
host.Run();
