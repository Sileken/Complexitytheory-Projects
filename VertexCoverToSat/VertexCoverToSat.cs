using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.Graph;
using Complexitytheory.Graph.VertexCover;
using Complexitytheory.SAT;

namespace VertexCoverToSat
{
    class VertexCoverToSat
    {
        static void Main(string[] args)
        {
            var undirectedGraph = new AdjacentMap
            {
                {"1", new List<string>() {"2", "7", "11"}},
                {"2", new List<string>() {"1", "3", "11"}},
                {"3", new List<string>() {"2", "4", "10"}},
                {"4", new List<string>() {"3", "5", "9"}},
                {"5", new List<string>() {"4", "6", "9"}},
                {"6", new List<string>() {"5", "7", "8"}},
                {"7", new List<string>() {"1", "6", "8"}},
                {"8", new List<string>() {"6", "7", "12"}},
                {"9", new List<string>() {"4", "5", "12"}},
                {"10", new List<string>() {"3", "12", "11"}},
                {"11", new List<string>() {"1", "2", "10"}},
                {"12", new List<string>() {"8", "9", "10"}}
            };
            int vertexCount = 7;

            // Cover (1,5,3)
            //var undirectedGraph = new AdjacentMap
            //{
            //    {"1", new List<string>() {"2", "4"}},
            //    {"2", new List<string>() { "1", "3", "5"}},
            //    {"3", new List<string>() {"2", "5", "6", "7"}},
            //    {"4", new List<string>() {"1"}},
            //    {"5", new List<string>() {"2", "3", "6"}},
            //    {"6", new List<string>() {"3", "5"}},
            //    {"7", new List<string>() {"3"}}
            //};
            //int vertexCount = 3;

            // Cover (5,3)
            //var undirectedGraph = new AdjacentMap
            //{
            //    {"2", new List<string>() { "3", "5"}},
            //    {"3", new List<string>() {"2", "5", "6", "7"}},
            //    {"5", new List<string>() {"2", "3", "6"}},
            //    {"6", new List<string>() {"3", "5"}},
            //    {"7", new List<string>() {"3"}}
            //};
            //int vertexCount = 2;

            var vertexCoverReducter = new VertexCoverReducer();
            Formula formula = vertexCoverReducter.ReduceVertexCoverToSat(undirectedGraph, vertexCount);
            Console.WriteLine(formula.ToString());

            Console.WriteLine();
            Console.WriteLine();

            //SatResolver satResolver = new SatResolver();

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            //var satisfiableInfo = satResolver.IsSatisfiable(formula);

            //stopwatch.Stop();
            //Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");

            //bool[] maxSatisfiebleAssignment = SatUtil.GetMinSatisfiebleAssignment(satisfiableInfo.SatisfiableAssignments);
            //if (satisfiableInfo.IsSatisfiable && maxSatisfiebleAssignment.Count(a => a) > 0)
            //{
            //    Console.WriteLine("\nThe vertex cover problem is satisfiable!");
            //    for (int i = 0; i < satisfiableInfo.VariableList.Count; i++)
            //    {
            //        Console.WriteLine("{0}:{1}", satisfiableInfo.VariableList[i].Name, maxSatisfiebleAssignment[i]);
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("\nThe vertex cover problem is not satisfiable!");
            //}

            Console.ReadLine();
        }
    }
}
