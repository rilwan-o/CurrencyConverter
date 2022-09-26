using CurrencyConverter.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CurrencyConverter.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        public DbSet<CurrencyConverterLog> CurrencyConverterLogs { get; set; }
    }
}
