using CurrencyExchangeService.Features.ExchangeRate.Models;

namespace CurrencyExchangeService.Features.ExchangeRate.Interfaces
{
    /// <summary>
    /// Интерфейс для взаимодействия с api курса валют 
    /// </summary>
    public interface IExchangeRateService
    {
        Task<ExchangeRateViewModel> GetExchangeRate(DateTime date);
        Task<decimal> ParseExchangeRateFromContent(string xml, string curCode);
    }
}
