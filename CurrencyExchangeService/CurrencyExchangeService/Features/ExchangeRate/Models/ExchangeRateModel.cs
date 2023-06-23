namespace CurrencyExchangeService.Features.ExchangeRate.Models
{
    /// <summary>
    /// Модель данных информации о курсе валюты
    /// </summary>
    public class ExchangeRateModel
    {
        /// <summary>
        /// Код валюты
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// Дата курса
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Курс валюты
        /// </summary>
        public decimal Rate { get; set; }
    }
}
