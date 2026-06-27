using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Settings;

public class RabbitMqSettings
{
    public string Host { get; set; } = string.Empty;
    public string VirtualHost { get; set; } = "/";
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string KeyQueueOrderPlaced { get; set; } = string.Empty;
    public string KeyPublisher { get; set; } = string.Empty;
}
