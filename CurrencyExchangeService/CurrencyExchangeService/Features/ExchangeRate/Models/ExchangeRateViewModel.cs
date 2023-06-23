namespace CurrencyExchangeService.Features.ExchangeRate.Models
{
    /// <summary>
    /// Модель представления даннхы о курсе валюты потребителю
    /// </summary>
    public class ExchangeRateViewModel
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
        /// <summary>
        /// Дополнительное сообщение
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public ExchangeRateViewModel()
        {
        }

        /// <summary>
        /// Конструктор по ошибочному сообщению
        /// </summary>
        public ExchangeRateViewModel(string message)
        {
            Date= DateTime.Now;
            Rate = 0;
            Message = message;
            CurrencyCode = string.Empty;
        }
    }
}
