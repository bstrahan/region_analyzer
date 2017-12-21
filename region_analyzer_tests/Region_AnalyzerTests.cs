using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using region_analyzer;

namespace region_analyzer_tests
{
    [TestClass]
    public class Region_AnalyzerTests
    {
        public float[,] grid;
        public float[,] staticGrid;

        [TestInitialize]
        public void Init()
        {
            grid = GridGenerator.GetTestGrid(10, 300.0f);
            staticGrid = new float[6, 6] { { 5, 0, 25, 5, 145, 250},
                                           { 0, 5, 95, 115, 115, 250},
                                           { 15, 5, 175, 250, 230, 160},
                                           { 5, 0, 145, 250, 245, 140},
                                           { 115, 210, 6, 5, 230, 220},
                                           { 0, 80, 45, 95, 170, 145}};
        }

        [TestMethod]
        public void WriteGridToConsole()
        {
            GridGenerator.WriteGridToConsole(staticGrid);
        }
    }

    [TestClass]
    public class ThresholdTests
    {
        public float[,] grid;

        [TestInitialize]
        public void Init()
        {
            grid = new float[6, 6] {{ 5, 0, 25, 5, 145, 250},
                                    { 0, 5, 95, 115, 115, 250},
                                    { 15, 5, 175, 250, 230, 160},
                                    { 5, 0, 145, 250, 245, 140},
                                    { 115, 210, 6, 5, 230, 220},
                                    { 0, 80, 45, 95, 170, 145}};
        }

        [TestMethod]
        public void RegionAnalyzer_IdentifiesExpectedContiguousRegions_AboveThreshold()
        {
            var analyzer = new RegionAnalyzer(grid, 200);
            var regionsOfInterest = analyzer.Analyze();

            Assert.AreEqual(2, regionsOfInterest.Count(), "Regions Identified Incorrect");

            var pointsInRegion = regionsOfInterest.FirstOrDefault().ComponentPoints;
            Assert.AreEqual(8, pointsInRegion.Count);
        }
    }

}
