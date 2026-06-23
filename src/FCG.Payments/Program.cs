

using FCG.Payments;
using FCG.Payments.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Services
    .ConfigureWorker()
    .ConfigureApplication()
    .ConfigureDomain()
    .ConfigureInfrastructure(builder.Configuration);

IHost host = builder.Build();
host.Run();
