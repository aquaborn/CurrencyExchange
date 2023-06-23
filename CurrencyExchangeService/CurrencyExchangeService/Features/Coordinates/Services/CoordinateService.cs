using CurrencyExchangeService.Features.Coordinates.Intrefaces;
using CurrencyExchangeService.Features.Coordinates.Models;

namespace CurrencyExchangeService.Features.Coordinates.Services
{
    public class CoordinateService : ICoordinateService
    {
        private readonly double _circleRadius;

        public CoordinateService(double circleRadius)
        {
            _circleRadius = circleRadius;
        }

        public CoordinateQuadrant GetCoordinateQuadrant(double x, double y)
        {
            if (x > 0 && y > 0)
            {
                return CoordinateQuadrant.I;
            }
            else if (x < 0 && y > 0)
            {
                return CoordinateQuadrant.II;
            }
            else if (x < 0 && y < 0)
            {
                return CoordinateQuadrant.III;
            }
            else if (x > 0 && y < 0)
            {
                return CoordinateQuadrant.IV;
            }
            else
            {
                return CoordinateQuadrant.Unknown;
            }
        }

        public bool IsInsideCircle(double x, double y)
        {
            double distance = Math.Sqrt(x * x + y * y);
            return distance <= _circleRadius;
        }
    }
}
