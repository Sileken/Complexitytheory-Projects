using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.Graph;
using Complexitytheory.Graph.VertexCover;

namespace VertexCover
{
    class VertexCover
    {
        static void Main(string[] args)
        {
            var undirectedGraph = new AdjacentMap
            {
                {"1", new List<string>() {"2", "3"}},
                {"2", new List<string>() {"4", "1"}},
                {"3", new List<string>() {"1", "5"}},
                {"4", new List<string>() {"2", "5", "6"}},
                {"5", new List<string>() {"3", "4", "6"}},
                {"6", new List<string>() {"4", "5"}}
            };

            VertexCoverResolver vertexCoverResolver = new VertexCoverResolver();

            List<string> smallestVertexCover = undirectedGraph.Keys.ToList();

            for(int i = 0; i < 20; i++)
            {
                var approximateMinVertexCover = vertexCoverResolver.ApproximateMinVertexCover(undirectedGraph);
                if (smallestVertexCover.Count > approximateMinVertexCover.Count)
                {
                    smallestVertexCover = approximateMinVertexCover;
                }

                Console.WriteLine($"Found Vertex Cover: {string.Join(", ", approximateMinVertexCover)}");
            }

            Console.WriteLine($"Found smallest Vertex Cover: {string.Join(", ", smallestVertexCover)}");

            Console.ReadLine();
        }
    }
}
