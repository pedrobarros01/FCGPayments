using FCG.Payments.Domain.Entities;
using FCG.Payments.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTestUtilities.Database;

public static class ContextBuilder
{
    public static async Task<ApplicationDbContext> GenerateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options);
        context.PaymentTransactionStatus.AddRange([
            new PaymentTransactionStatus(
            Guid.Parse("11111111-1111-1111-1111-111111111111"),
            "Aprovado",
            "Pagamento aprovado"
            ),
            new PaymentTransactionStatus(
            Guid.Parse("22222222-2222-2222-2222-222222222222"),
            "Reprovado",
            "Pagamento reprovado"
            )
        ]);
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return context;
    }
}
