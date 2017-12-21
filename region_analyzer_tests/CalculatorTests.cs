using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using region_analyzer;

namespace region_analyzer_tests
{
    [TestClass]
    public class CenterOfMassCoordinateCalculatorTests
    {
        CenterOfMassCoordinateCalculator massCalculator;

        [TestInitialize]
        public void Init()
        {
            massCalculator = new CenterOfMassCoordinateCalculator();
        }

        [TestMethod]
        public void CalculatorTests_ReturnsNull_EmptyOrNull()
        {
            var centerOfMass = massCalculator.Calc(null);
            Assert.AreEqual(null, centerOfMass);

            centerOfMass = massCalculator.Calc(new List<Coordinate>());
            Assert.AreEqual(null, centerOfMass);
        }

        [TestMethod]
        public void CalculatorTests_ReturnsExpected_Coordinates_Simple_X()
        {
            var points = new List<Coordinate>
            {
                new Coordinate(0, 0, 10),
                new Coordinate(1, 0, 10),
                new Coordinate(2, 0, 10),
                new Coordinate(3, 0, 10),
            };

            var expected = new Tuple<decimal, decimal>(1.5m, 0m);
            AssertCalculatedCoordinatesCorrect(expected, points);
        }

        [TestMethod]
        public void CalculatorTests_ReturnsExpected_Coordinates_Simple_Y()
        {
            var points = new List<Coordinate>
            {
                new Coordinate(0, 0, 10),
                new Coordinate(0, 1, 10),
                new Coordinate(0, 2, 10),
                new Coordinate(0, 3, 10),
            };

            var expected = new Tuple<decimal, decimal>(0m, 1.5m);
            AssertCalculatedCoordinatesCorrect(expected, points);
        }

        [TestMethod]
        public void CalculatorTests_ReturnsExpected_Coordinates_Simple_XY()
        {
            var points = new List<Coordinate>
            {
                new Coordinate(0, 0, 10),
                new Coordinate(0, 1, 10),
                new Coordinate(1, 1, 10),
                new Coordinate(1, 0, 10),
            };
            var expected = new Tuple<decimal, decimal>(.5m, .5m);
            AssertCalculatedCoordinatesCorrect(expected, points);
        }

        [TestMethod]
        public void CalculatorTests_ReturnsExpected_Coordinates_Moderate_XY()
        {
            var points = new List<Coordinate>
            {
                new Coordinate(1, 2, 10),
                new Coordinate(2, 1, 10),
                new Coordinate(2, 0, 10),
                new Coordinate(3, 0, 10),
                new Coordinate(4, 1, 10),
            };

            var expected = new Tuple<decimal, decimal>(2.4m, .8m);
            AssertCalculatedCoordinatesCorrect(expected, points);
        }

        [TestMethod]
        public void CalculatorTests_ReturnsExpected_Coordinates_Complex_XY()
        {
            var points = new List<Coordinate>
            {
                new Coordinate(1, 2, 10),
                new Coordinate(2, 1, 200),
                new Coordinate(2, 0, 35),
                new Coordinate(3, 0, 43),
                new Coordinate(4, 1, 6),
            };

            var expected = new Tuple<decimal, decimal>(2.153m, 0.769m);
            AssertCalculatedCoordinatesCorrect(expected, points);
        }

        private void AssertCalculatedCoordinatesCorrect(Tuple<decimal, decimal> expected, List<Coordinate> points)
        {
            var actual = massCalculator.Calc(points);
            AssertCentorOfMassIsWithinBounds(actual, points);
            Assert.AreEqual(expected.Item1, actual.Item1, $"Center of Mass Unexpected: X-Coord");
            Assert.AreEqual(expected.Item2, actual.Item2, $"Center of Mass Unexpected: Y-Coord");
        }

        private void AssertCentorOfMassIsWithinBounds(Tuple<decimal, decimal> actual, List<Coordinate> sourceCoordinates)
        {
            var min_x = (decimal) sourceCoordinates.Min(p => p.X);
            var max_x = (decimal) sourceCoordinates.Max(p => p.X);

            var min_y = (decimal) sourceCoordinates.Min(p => p.Y);
            var max_y = (decimal) sourceCoordinates.Max(p => p.Y);

            Assert.IsTrue(min_x <= actual.Item1 && actual.Item1 <= max_x, $"X-Range: {min_x} - {max_x}\nX: {actual.Item1}");
            Assert.IsTrue(min_y <= actual.Item2 && actual.Item2 <= max_y, $"Y-Range: {min_y} - {max_y}\nX: {actual.Item2}");
        }

    }
}
