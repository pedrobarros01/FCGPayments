using CommonTestUtilities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Tests.Fixture;

[CollectionDefinition("PaymentTransactionFixtureCollection")]
public class PaymentTransactionFixtureCollection : ICollectionFixture<PaymentTransactionBuilder>
{
}
