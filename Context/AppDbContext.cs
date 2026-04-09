using Microsoft.EntityFrameworkCore;
using BitcoinApi.Entities;

namespace BitcoinApi.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Bitcoin> Bitcoins => Set<Bitcoin>();
}