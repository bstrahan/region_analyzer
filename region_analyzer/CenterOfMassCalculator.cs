using System;
using System.Collections.Generic;
using System.Linq;

namespace region_analyzer
{
    public interface ICoordinateCalculator
    {
        Tuple<decimal, decimal> Calc(IEnumerable<Coordinate> elements);
    }

    public class CenterOfMassCoordinateCalculator : ICoordinateCalculator
    {
        private int numDecimals;

        public CenterOfMassCoordinateCalculator() : this(3) { }

        public CenterOfMassCoordinateCalculator(int decimals)
        {
            this.numDecimals = decimals;
        }

        public Tuple<decimal, decimal> Calc(IEnumerable<Coordinate> elements)
        {
            if (elements == null || !elements.Any()) {
                return null;
            }

            //assuming each coordinate has a constant density and an area of 1
            //we can express the center of mass for a dimension 'x' as:
            // m1x1 + m2x2 + ... + mnxn
            // -------------------------
            //   m1 + m2 + ... + mn

            //don't let 0-based indices corrupt CoM calculations;
            //shift x-and y indices by + 1;
            var preppedCoordinates = elements.Select(p => new Coordinate(p.X + 1, p.Y + 1, p.Z)).ToList();

            //calculate CoM
            var totalMass = preppedCoordinates.Sum(p => p.Z);
            var x_c = (decimal)(preppedCoordinates.Sum(p => (p.X * p.Z)) / totalMass);
            var y_c = (decimal)(preppedCoordinates.Sum(p => (p.Y * p.Z)) / totalMass);

            //shift back to 0 - based indices, limit decimals as desired
            return new Tuple<decimal, decimal>(
                                Decimal.Round(x_c - 1, numDecimals),
                                Decimal.Round(y_c - 1, numDecimals));
        }
    }
}
