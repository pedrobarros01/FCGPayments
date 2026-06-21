using FCG.Payments.Domain.Interfaces.Repositories;
using FCG.Payments.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Payments.Infrastructure.Persistence;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _context = context;
    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
