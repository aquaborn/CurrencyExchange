using CurrencyExchangeService.Features.ExchangeRate.Interfaces;
using CurrencyExchangeService.Features.ExchangeRate.Models;
using System;
using System.Globalization;
using System.Text;
using System.Xml;
using static DailyInfoService.DailyInfoSoapClient;

namespace CurrencyExchangeService.Features.ExchangeRate.Services
{
    /// <summary>
    /// Сервис для получения курса валюты
    /// </summary>
    public class ExchangeRateService : IExchangeRateService
    {        
        private readonly AppSettings _appSettings;

        public ExchangeRateService(AppSettings appSettings)
        {            
            _appSettings = appSettings;
        }

        /// <summary>
        /// Получение курса валюты по дате
        /// </summary>
        /// <param name="date">Дата подсчета курса валюты</param>
        /// <returns></returns>
        public async Task<ExchangeRateViewModel> GetExchangeRate(DateTime date)
        {
            string? message = null;
            decimal rate = 0;
            try
            {
                DailyInfoService.DailyInfoSoap client = new DailyInfoService.DailyInfoSoapClient(EndpointConfiguration.DailyInfoSoap);
                var response = await client.GetCursOnDateXMLAsync(date);

                var content = response.OuterXml;                    
                rate = await ParseExchangeRateFromContent(content, _appSettings.CurrencyCode);  
                if(rate == 0)
                {
                    message = "Нет данных о курсе";
                }                
            }
            catch (Exception ex)
            {
                message = "Ресурс недоступен";
            }
            return new ExchangeRateViewModel
            {
                CurrencyCode = _appSettings.CurrencyCode,
                Date = date,
                Rate = rate,
                Message = message
            };
        }

        /// <summary>
        /// Парсинг курса валюты из xml ответа веб-службы
        /// </summary>
        /// <param name="xml">Получений xml от внешней веб-службы</param>
        /// <param name="curCode">Код валюты</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<decimal> ParseExchangeRateFromContent(string xml, string curCode)
        {            
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            XmlNodeList valuteCursList = xmlDoc.SelectNodes("//ValuteCursOnDate");

            foreach (XmlNode valuteCursNode in valuteCursList)
            {
                XmlNode vchCodeNode = valuteCursNode.SelectSingleNode("VchCode");
                XmlNode vcursNode = valuteCursNode.SelectSingleNode("Vcurs");

                string vchCode = vchCodeNode?.InnerText;
                string vcurs = vcursNode?.InnerText;

                if (!string.IsNullOrEmpty(vchCode) && !string.IsNullOrEmpty(vcurs) && vchCode == curCode)
                {
                    if (decimal.TryParse(vcurs, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal exchangeRate))
                    {
                        return exchangeRate;
                    }
                }
            }

            return 0;
            
        }
        
    }
    
}
