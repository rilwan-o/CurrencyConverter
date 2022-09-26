using CurrencyConverter.Data;
using CurrencyConverter.Dtos;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Models;

namespace CurrencyConverter.Services
{
    public class ConverterService : IConverterService
    {
        private readonly AppDbContext _dbContext;
        public ConverterService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal Convert(ConverterModel model)
        {
            var currency = _dbContext.Currencies.FirstOrDefault(c => c.Code.Equals(model.ToCurrencyCode));
            if (currency == null) return 0;
            var rate = _dbContext.CurrencyRates.FirstOrDefault(r => r.CurrencyId == currency.Id);
            var result = rate.Rate * model.FromValue;
            CurrencyConverterLog currencyConverterLog = new CurrencyConverterLog
            {
                CreatedAt = DateTime.Now,
                FromValue = model.FromValue,
                ToCurrency = currency.Code,
                Rate = rate.Rate,
                ToValue = result
            };

            Log(currencyConverterLog);

            return result;
        }

        public void Log(CurrencyConverterLog model)
        {
            _dbContext.CurrencyConverterLogs.Add(model);
            _dbContext.SaveChanges();
        }

        public List<CurrencyConverterLog> queryLog(DateTime from, DateTime to)
        {
            //var logg = _dbContext.CurrencyConverterLogs.ToList();
            return _dbContext.CurrencyConverterLogs.Where(a => a.CreatedAt >= from && a.CreatedAt <= to).ToList();
        }
    }
}
