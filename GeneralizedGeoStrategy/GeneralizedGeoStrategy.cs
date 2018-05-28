using System;
using System.Collections.Generic;
using System.Diagnostics;
using Complexitytheory.Graph;
using Complexitytheory.Graph.GeneralizedGeography;

namespace GeneralizedGeoStrategy
{
    class GeneralizedGeoStrategy
    {
        static void Main(string[] args)
        {
            // Let GG = { <G, b> | P1 has a winning strategy for the generalized geography game played on graph G starting at node b };

            // 1 -> 2 -> 4 -> 5 -> 3 -> 9
            // 1 -> 2 -> 4 -> 5 -> 7 -> 9
            var dGraph = new AdjacentMap
            {
                {"1", new List<string>() {"2", "3"}},
                {"2", new List<string>() {"4"}},
                {"3", new List<string>() {"9"}},
                {"4", new List<string>() {"5", "7"}},
                {"5", new List<string>() {"3", "7"}},
                {"6", new List<string>() {"2", "7"}},
                {"7", new List<string>() {"8", "9"}},
                {"8", new List<string>() {"6", "9"}},
                {"9", new List<string>() {}}
            };

            // 1 -> 3 -> 5 -> 6
            //var dGraph = new AdjacentMap
            //{
            //    {"1", new List<string>() {"2", "3"}},
            //    {"2", new List<string>() {"3", "4" ,"5"}},
            //    {"3", new List<string>() {"5"}},
            //    {"4", new List<string>() {"5", "7"}},
            //    {"5", new List<string>() {"6", "7", "8"}},
            //    {"6", new List<string>() {"3"}},
            //    {"7", new List<string>() {"8", "9"}},
            //    {"8", new List<string>() {"6", "9"}},
            //    {"9", new List<string>() {}}
            //};

            // No Strategy
            //var dGraph = new AdjacentMap()
            //{
            //    {"1", new List<string>() {"2", "3"}},
            //    {"2", new List<string>() {"4"}},
            //    {"3", new List<string>() {"4"}},
            //    {"4", new List<string>() },
            //};

            Console.WriteLine($"Try to find a winning strategy for Player 1.");

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var strategyInfo = GeneralizedGeoStrategyFinder.FindStrategy(dGraph, "1");

            stopwatch.Stop();
            Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");

            Console.WriteLine($"{Console.Out.NewLine}Player 1 found a winning strategy: {strategyInfo.FoundStrategy}");
            foreach (List<string> strategy in strategyInfo.Strategies)
            {
                Console.WriteLine($"Strategy: {String.Join(" -> ", strategy)}");
            }

            Console.ReadLine();
        }
    }
}
