using System.Collections;

namespace CurrencyConverter.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        
    }

    public class CurrencyRate
    {
        public int Id { get; set; }
        public int CurrencyId { get; set; }
        public decimal Rate { get; set; }
    }

    

    public class CurrencyConverterLog
    {
        public int Id { get; set; }
        public decimal Rate { get; set; }
        public string FromCurrency { get; set; } = "GBP";
        public decimal FromValue { get; set; }
        public string ToCurrency { get; set; }
        public decimal ToValue { get; set; }
        public DateTime CreatedAt { get; set; }

    }

    
}
