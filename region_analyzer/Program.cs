using System;

namespace region_analyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            var exampleGrid = new float[6, 6] {{ 0, 80, 45, 95, 170, 145},
                                               { 115, 210, 6, 5, 230, 220},
                                               { 5, 0, 145, 250, 245, 140},
                                               { 15, 5, 175, 250, 185, 160},
                                               { 0, 5, 95, 115, 115, 250},
                                               { 5, 0, 25, 5, 145, 250} };

            Console.WriteLine("Let's analyze the following Grid:\n");
            GridGenerator.WriteGridToConsole(exampleGrid);

            while (true) {

                var threshold = PromptForThreshold();
                AnalyzeRegion(exampleGrid, threshold);

                PromptToContinue();
            }
        }

        static void AnalyzeRegion(float[,] grid, float threshold)
        {
            var analyzer = new RegionAnalyzer(grid, threshold);
            var regionsAboveThreshold = analyzer.Analyze();

            foreach(var region in regionsAboveThreshold) {
                Console.WriteLine(region.GetSummary() + "\n");
            }
        }

        static float PromptForThreshold()
        {
            Console.Write("\nThreshold: ");
            var input = Console.ReadLine();
            if (!float.TryParse(input, out float threshold)) {
                Console.Write("\nNot a valid threshold.  Exiting.");
                Environment.Exit(-1);
            }
            return threshold;
        }

        static void PromptToContinue()
        {
            Console.Write("\nAgain? ");
            var input = Console.ReadKey();
            if (input.KeyChar == 'y' || input.KeyChar == 'Y') {
                return;
            }
            Environment.Exit(-1);
        }
    }
}
