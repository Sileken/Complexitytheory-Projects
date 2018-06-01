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
            //var graph = new AdjacentMap()
            //{
            //    {"A", new List<string>() {"B"}},
            //    {"B", new List<string>() {"C"}},
            //    {"C", new List<string>() {"D"}},
            //    {"D", new List<string>() {"E"}}
            //};

            //int steps = 5; // 2^steps
            //string startVertex = "A";
            //string endVertex = "E";

            var graph = new AdjacentMap
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

            int steps = 2; // 2^steps
            string startVertex = "1";
            string endVertex = "5";

            Console.WriteLine($"Check if vertex {endVertex} is reachable from vertex {startVertex}");
            VertexReachabilityChecker vertexReachabilityChecker = new VertexReachabilityChecker();

            Stopwatch stopwatch = new Stopwatch();

            //2^0 = 1
            var vertexReachabilityInfo = vertexReachabilityChecker.CheckReachability(graph, steps, startVertex, endVertex);

            stopwatch.Stop();
            Console.WriteLine($"{Console.Out.NewLine}Time elapsed: {stopwatch.Elapsed}");
            
            if (!vertexReachabilityInfo.IsReachable)
            {
                Console.WriteLine($"\nVertex {endVertex} is not reachable from vertex {startVertex}.");
            }
            else
            {
                Console.WriteLine($"\nVertex {endVertex} is reachable from vertex {startVertex}.");
                Console.WriteLine($"Path: {string.Join("->", vertexReachabilityInfo.Path)}");
            }

            Console.Out.Flush();
            Console.ReadLine();
        }
    }
}
