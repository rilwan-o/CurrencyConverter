using CurrencyConverter.Dtos;
using CurrencyConverter.Models;

namespace CurrencyConverter.Interfaces
{
    public interface IConverterService
    {
        decimal Convert(ConverterModel model);
        void Log(CurrencyConverterLog model);

        List<CurrencyConverterLog> queryLog(DateTime from, DateTime to);

    }
}
