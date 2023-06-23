using CurrencyExchangeService.Features.Coordinates.Models;

namespace CurrencyExchangeService.Features.Coordinates.Intrefaces
{
    public interface ICoordinateService
    {
        CoordinateQuadrant GetCoordinateQuadrant(double x, double y);
        bool IsInsideCircle(double x, double y);
    }
}
