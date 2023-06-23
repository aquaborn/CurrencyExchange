using CurrencyExchangeService.Features.ExchangeRate.Interfaces;
using CurrencyExchangeService.Features.ExchangeRate.Models;
using System;

namespace CurrencyExchangeService.Features.ExchangeRate.Services
{
    /// <summary>
    /// Сервис для получения курса валюты
    /// </summary>
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IExchangeRateService _exchangeRateApiService;
        private readonly AppSettings _appSettings;

        public ExchangeRateService(IExchangeRateService exchangeRateApiService, AppSettings appSettings )
        {
            _exchangeRateApiService = exchangeRateApiService;
            _appSettings = appSettings;
        }

        public async Task<ExchangeRateViewModel> GetExchangeRate(DateTime date)
        {
            string? message = null;
            decimal rate = 0;
            try
            {
                using var httpClient = new HttpClient();
                // Выполнение GET-запроса к внешнему URL из конфигурации
                var response = await httpClient.GetAsync(_appSettings.ExternalAPI);

                if (response.IsSuccessStatusCode)
                {
                    // Получение контента ответа
                    var content = await response.Content.ReadAsStringAsync();
                    // Обработка контента ответа, парсинг и извлечение данных о курсе валюты
                    //rate = ParseExchangeRateFromContent(content, _appSettings.CurrencyCode);               
                }
                else
                    message = "Нет данных о курсе";
            }
            catch (Exception ex)
            {
                message = "Ресурс недоступен";
            }
            //GetCursOnDate(date)
            return new ExchangeRateViewModel
            {
                CurrencyCode = _appSettings.CurrencyCode,
                Date = date,
                Rate = rate,
                Message = message
            };
        }
    }
}
