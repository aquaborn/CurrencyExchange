using CurrencyExchangeService.Features.Coordinates.Intrefaces;
using CurrencyExchangeService.Features.Coordinates.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchangeService.Features.Coordinates.Controllers
{
    [ApiController]
    [Route("api/coordinates")]
    public class CoordinateController : ControllerBase
    {
        private readonly ICoordinateService _coordinateService;

        public CoordinateController(ICoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }

        [HttpGet]
        public ActionResult<string> Get(double x, double y)
        {
            var quadrant = _coordinateService.GetCoordinateQuadrant(x, y);
            var insideCircle = _coordinateService.IsInsideCircle(x, y);

            if (quadrant == CoordinateQuadrant.Unknown || !insideCircle)
            {
                return NotFound("Координаты находятся вне круга или некорректны.");
            }

            return Ok($"Квандрат: {quadrant}");
        }
    }
}
