using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Complexitytheory.Graph;
using Complexitytheory.Graph.IndependentSet;
using Complexitytheory.SAT;
using Complexitytheory.SAT.FormulaComponents;

namespace Wichtel_Independent_Set
{
    class WichtelIndependentSet
    {
        private static readonly Operator And = new Operator(Operator.Types.And);
        private static readonly Operator Or = new Operator(Operator.Types.Or);
        private static readonly Operator Not = new Operator(Operator.Types.Not);

        static void Main(string[] args)
        {
            AdjacentMap wichtelAdjazentMap = GetWichtelAdjazentMap();
            Dictionary<String, Variable> variableStore = new Dictionary<string, Variable>();
            Formula wichtel3Sat = IndependentSetReducer.ReduceTo3Sat(wichtelAdjazentMap, variableStore);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var satisfiableInfo = new SatResolver().IsSatisfiable(wichtel3Sat);

            stopwatch.Stop();
            Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");

            bool[] maxSatisfiebleAssignment = SatUtil.GetMaxSatisfiebleAssignment(satisfiableInfo.SatisfiableAssignments);
            if (satisfiableInfo.IsSatisfiable && maxSatisfiebleAssignment.Count(a => a) > 0)
            {
                Console.WriteLine("\nResolved Wichtel Independet Set!");
                for (int i = 0; i < satisfiableInfo.VariableList.Count; i++)
                {
                    Console.WriteLine("Wichtel {0}: {1}", satisfiableInfo.VariableList[i].Name, maxSatisfiebleAssignment[i]);
                }
            }
            else
            {
                Console.WriteLine("\nCould not resolve Wichtel Independet Set!");
            }

            Console.ReadLine();
        }

        private static AdjacentMap GetWichtelAdjazentMap()
        {
            AdjacentMap adjacentMap = new AdjacentMap();
            
            IEnumerable<string[]> lines = File.ReadAllLines("wichtel.txt").Select(a => a.Split(' '));

            foreach (var line in lines)
            {
                string nodeLabe = line[0];

                if (!adjacentMap.ContainsKey(nodeLabe))
                {
                    List<string> neighbors = new List<string>();

                    for (int i = 1; i < line.Length; i++)
                    {
                        if (!neighbors.Contains(line[i]))
                        {
                            neighbors.Add(line[i]);
                        }
                    }
                    
                    adjacentMap.Add(nodeLabe, neighbors);
                }
            }

            return adjacentMap;
        }
    }
}
