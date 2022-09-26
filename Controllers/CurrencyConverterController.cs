using CurrencyConverter.Data;
using CurrencyConverter.Dtos;
using CurrencyConverter.Interfaces;
using CurrencyConverter.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConverterController : ControllerBase
    {
        private readonly ILogger<CurrencyConverterController> _logger;

        private readonly IConverterService _converterService;
        private readonly AppDbContext _appDbContext;
        public CurrencyConverterController(IConverterService converterService,
            ILogger<CurrencyConverterController> logger, AppDbContext appDbContext)
        {
            _converterService = converterService;
            _logger = logger;
            _appDbContext = appDbContext;
            var currencies = new List<Currency>
            {
                new Currency
                {
                    Code = "USD"
                },
                new Currency
                {
                    Code = "AUD"
                },
                new Currency
                {
                    Code = "EUR"
                }
            };
            _appDbContext.Currencies.AddRange(currencies);
            _appDbContext.SaveChanges();

            var rates = new List<CurrencyRate>
            {
                new CurrencyRate
                {
                    CurrencyId = 1,
                    Rate = 0.75M
                },
                 new CurrencyRate
                {
                    CurrencyId = 2,
                    Rate = 0.65M
                },
                    new CurrencyRate
                {
                    CurrencyId = 2,
                    Rate = 0.5M
                }
            };

            _appDbContext.CurrencyRates.AddRange(rates);
            _appDbContext.SaveChanges();

        }

        [HttpPost("Convert")]
        public IActionResult Convert(ConverterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            try
            {
               var result =  _converterService.Convert(model);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }

        [HttpPost("QueryLog")]
        public IActionResult QueryLog(LogQueryModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }

            try
            {
                var result = _converterService.queryLog(model.from, model.to);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(500);
            }
        }
    }
}
