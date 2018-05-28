using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Complexitytheory.Graph;
using Complexitytheory.Graph.ThreeColoring;
using Complexitytheory.SAT;

namespace ThreeColoring
{
    internal class ThreeColoring
    {
        private static void Main(string[] args)
        {
            var graph = new AdjacentMap()
            {
                {"A", new List<string>() {"B", "F", "C", "E"}},
                {"B", new List<string>() {"A", "C", "D", "F"}},
                {"C", new List<string>() {"B", "D", "A", "E"}},
                {"D", new List<string>() {"C", "E", "B", "F"}},
                {"E", new List<string>() {"D", "F", "C", "A"}},
                {"F", new List<string>() {"E", "A", "B", "D"}}
            };

            Formula formula = ThreeColoringReducer.ReduceTo3Sat(graph);
            Console.WriteLine("Formula:");
            Console.WriteLine(formula);

            var satResolver = new SatResolver();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var satisfiableInfo = satResolver.IsSatisfiable(formula);

            stopwatch.Stop();
            Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");

            bool[] maxSatisfiebleAssignment = SatUtil.GetMaxSatisfiebleAssignment(satisfiableInfo.SatisfiableAssignments);
            if (satisfiableInfo.IsSatisfiable && maxSatisfiebleAssignment.Count(a => a) > 0)
            {
                Console.WriteLine("\nThe graph is 3-colorable!");
                for (int i = 0; i < satisfiableInfo.VariableList.Count; i++)
                {
                    var nodeColorInfo = satisfiableInfo.VariableList[i].Name
                        .Split("|", StringSplitOptions.RemoveEmptyEntries);
                    Console.WriteLine("Node {0}|{1}:{2}", nodeColorInfo[0], nodeColorInfo[1],
                        maxSatisfiebleAssignment[i]);
                }
            }
            else
            {
                Console.WriteLine("\nThe graph is not 3-colorable!");
            }

            Console.Out.Flush();
            Console.ReadLine();
        }
    }
}