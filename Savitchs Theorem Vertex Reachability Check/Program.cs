using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.Graph;
using Complexitytheory.Graph.VertexReachability;

namespace SavitchsTheoremVertexReachabilityCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new AdjacentMap()
            {
                {"A", new List<string>() {"B"}},
                {"B", new List<string>() {"C"}},
                {"C", new List<string>() {"D"}},
                {"D", new List<string>() {"E"}}
            };

            int steps = 5; // 2^steps
            string startVertex = "A";
            string endVertex = "E";

            Console.WriteLine($"Check if vertex {endVertex} is reachable from vertex {startVertex}");
            VertexReachabilityChecker vertexReachabilityChecker = new VertexReachabilityChecker();

            Stopwatch stopwatch = new Stopwatch();

            //2^0 = 1
            bool isReachable = vertexReachabilityChecker.CheckReachability(graph, steps, startVertex, endVertex);

            stopwatch.Stop();
            Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");
            
            if (!isReachable)
            {
                Console.WriteLine($"\nVertex {endVertex} is not reachable from vertex {startVertex}.");
            }
            else
            {
                Console.WriteLine($"\nVertex {endVertex} is reachable from vertex {startVertex}.");
            }

            Console.Out.Flush();
            Console.ReadLine();
        }
    }
}
