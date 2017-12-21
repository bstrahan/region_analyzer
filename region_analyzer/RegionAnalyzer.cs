using System.Collections.Generic;
using System.Linq;

namespace region_analyzer
{
    public class RegionAnalyzer
    {
        private float[,] Grid { get; set; }
        private bool[,] AnalyzedGrid { get; set; }

        public float Threshold { get; set; }

        public List<InterestingRegion> Regions { get; set; }

        public RegionAnalyzer(float[,] grid, float threshold)
        {
            Grid = grid;
            Threshold = threshold;
            AnalyzedGrid = new bool[grid.GetLength(0), grid.GetLength(1)];
        }

        public List<InterestingRegion> Analyze()
        {
            var regions = new List<InterestingRegion>();

            for (var x = 0; x < Grid.GetLength(0); x++) {
                for (var y = 0; y < Grid.GetLength(1); y++) {
                    if (AnalyzedGrid[x, y]) {
                        continue;
                    }

                    AnalyzedGrid[x, y] = true;
                    if (Grid[x, y] > Threshold) {

                        var point = new Coordinate(x, y, Grid[x, y]);
                        var adjacencyTree = new List<Coordinate> { point }
                                            .Concat(AnalyzeAdjacentPoints(point))
                                            .ToList();

                        regions.Add(new InterestingRegion(Grid, new CenterOfMassCoordinateCalculator())
                        {
                            ComponentPoints = adjacencyTree
                        });
                    }
                }
            }

            regions.ForEach(r => r.DetermineCenterOfMass());

            Regions = regions;

            return Regions;
        }

        private List<Coordinate> AnalyzeAdjacentPoints(Coordinate point)
        {
            List<Coordinate> adjacentPoints = new List<Coordinate>();

            int[] adjXIndices = new[] { point.X - 1, point.X, point.X + 1 };
            int[] adjYIndices = new[] { point.Y - 1, point.Y, point.Y + 1 };
            
            foreach(var x in adjXIndices) {
                foreach(var y in adjYIndices) {
                    if (IsValidXCoordinate(x) && 
                        IsValidYCoordinate(y)) {
                        if (AnalyzedGrid[x, y]) {
                            continue;
                        }

                        AnalyzedGrid[x, y] = true;
                        if (Grid[x,y] > Threshold) {
                            var adjacentPoint = new Coordinate(x, y, Grid[x, y]);
                            adjacentPoints.Add(adjacentPoint);
                            adjacentPoints = adjacentPoints.Concat(AnalyzeAdjacentPoints(adjacentPoint)).ToList();
                        }
                    }
                }
            }

            return adjacentPoints;
        }

        private bool IsValidXCoordinate(int x)
        {
            return x >= 0 && x <= Grid.GetLength(0) - 1;
        }

        private bool IsValidYCoordinate(int y)
        {
            return y >= 0 && y <= Grid.GetLength(1) - 1;
        }
    }

}
