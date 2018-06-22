using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;

namespace Complexitytheory.Graph.TravelingSalesmanProblem
{
    public class TravelingSalesmanResolver
    {
        public List<string> ApproximateWith2Approximation(
            UndirectedGraph<string, TaggedUndirectedEdge<string, int>> pUndirectedCompletedGraph, string pStartVertex)
        {
            List<string> verticePath = new List<string>();

            var minimumSpanningTree = pUndirectedCompletedGraph.MinimumSpanningTreePrim(e => e.Tag).ToList();

            var minimumSpanningTreeGraph = new UndirectedGraph<string, TaggedUndirectedEdge<string, int>>();
            minimumSpanningTreeGraph.AddVerticesAndEdgeRange(minimumSpanningTree);

            var depthFirstSearch = new UndirectedDepthFirstSearchAlgorithm<string, TaggedUndirectedEdge<string, int>>(minimumSpanningTreeGraph);          

            depthFirstSearch.DiscoverVertex +=  delegate(string vertex) {
                if (!verticePath.Contains(vertex))
                {
                    verticePath.Add(vertex);
                } };

            depthFirstSearch.Compute(pStartVertex);

            verticePath.Add(pStartVertex);

            return verticePath;
        }
    }
}
