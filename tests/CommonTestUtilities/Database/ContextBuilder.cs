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
        await context.SaveChangesAsync();
        context.ChangeTracker.Clear();
        return context;
    }
}
