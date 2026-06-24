using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Settings;

public class FCGSettings
{
    public ConnectionStrings ConnectionStrings { get; set; } = new();
    public RabbitMqSettings RabbitMQ { get; set; } = new();
}
