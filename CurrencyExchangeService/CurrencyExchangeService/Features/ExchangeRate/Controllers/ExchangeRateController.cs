using CurrencyExchangeService.Features.Coordinates.Intrefaces;
using CurrencyExchangeService.Features.Coordinates.Models;
using CurrencyExchangeService.Features.ExchangeRate.Interfaces;
using CurrencyExchangeService.Features.ExchangeRate.Models;
using CurrencyExchangeService.Features.ExchangeRate.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeService.Features.ExchangeRate.Controllers
{
    [ApiController]
    [Route("api/exchangerate")]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ICoordinateService _coordinateService;

        public ExchangeRateController(IExchangeRateService exchangeRateService, ICoordinateService coordinateService)
        {
            _exchangeRateService = exchangeRateService;
            _coordinateService = coordinateService;
        }

        [HttpGet]
        public async Task<ActionResult<ExchangeRateViewModel>> GetExchangeRate(string currencyCode, double x, double y)
        {
            var quadrant = _coordinateService.GetCoordinateQuadrant(x, y);
            var insideCircle = _coordinateService.IsInsideCircle(x, y);

            if (quadrant == CoordinateQuadrant.Unknown || !insideCircle)
            {
                return NotFound("Некорректные координаты квадранта");
            }

            DateTime date;

            switch (quadrant)
            {
                case CoordinateQuadrant.I:
                    date = DateTime.Today;
                    break;
                case CoordinateQuadrant.II:
                    date = DateTime.Today.AddDays(-1);
                    break;
                case CoordinateQuadrant.III:
                    date = DateTime.Today.AddDays(-2);
                    break;
                case CoordinateQuadrant.IV:
                    date = DateTime.Today.AddDays(1);
                    break;
                default:
                    return BadRequest("Некорректные координаты квадранта.");
            }

            var exchangeRate = await _exchangeRateService.GetExchangeRate(currencyCode, date);

            if (exchangeRate == null)
            {
                return NotFound("Курс недоступен на заданную дату.");
            }

            var viewModel = new ExchangeRateViewModel
            {
                CurrencyCode = exchangeRate.CurrencyCode,
                Date = exchangeRate.Date,
                Rate = exchangeRate.Rate
            };

            return Ok(viewModel);
        }
    }
}
