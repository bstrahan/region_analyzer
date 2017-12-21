using System;

namespace region_analyzer_tests
{
    public static class GridGenerator
    {

        public static float[,] GetTestGrid(int n, float maxPointValue)
        {
            return GetTestGrid(n, n, maxPointValue);
        }

        public static float[,] GetTestGrid(int m, int n, float maxPointValue)
        {
            var grid = new float[m, n];
            var rand = new Random();

            for(int r = 0; r < m; r++) {
                for(var i = 0; i < n; i++) {

                    grid[r, i] = (float)rand.NextDouble() * maxPointValue;
                }
            }

            return grid;
        }

        public static void WriteGridToConsole(float[,] grid)
        {
            for (var x = 0; x < grid.GetLength(0); x++) {
                Console.Write("|");
                for (var y = 0; y < grid.GetLength(1); y++) {
                    Console.Write($"   {grid[x, y].ToString("000.00")}   |");
                }
                Console.WriteLine("\t");
            }
        }

        public static void WriteGridToConsole(bool[,] grid)
        {
            for (var x = 0; x < grid.GetLength(0); x++) {
                Console.Write("|");
                for (var y = 0; y < grid.GetLength(1); y++) {
                    Console.Write($"{grid[x,y],9}");
                }
                Console.WriteLine("\t");
            }
        }
    }
}
