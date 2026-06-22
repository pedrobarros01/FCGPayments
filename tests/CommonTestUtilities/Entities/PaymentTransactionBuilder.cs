using Bogus;
using FCG.Payments.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Entities;

public class PaymentTransactionBuilder
{
    public PaymentTransaction GenerateTransactionWithStatusEmpty()
    {
        var paymentTransaction = Build();
        return paymentTransaction;
    }

    private PaymentTransaction Build()
    {
        var paymentTransaction = new Faker<PaymentTransaction>("pt_BR")
            .RuleFor(p => p.Id, faker => faker.Random.Guid())
            .RuleFor(p => p.GameId, faker => faker.Random.Guid())
            .RuleFor(p => p.UserId, faker => faker.Random.Guid())
            .RuleFor(p => p.DateTransaction, faker => faker.Date.Recent())
            .RuleFor(p => p.Price, faker => faker.Random.Decimal(10.50m, 500.75m));
        return paymentTransaction;
    }
}
