using System;
using System.Collections.Generic;
using Complexitytheory.Graph.TravelingSalesmanProblem;
using QuickGraph;

namespace Traveling_Salesman_Problem
{
    internal class TravelingSalesMan
    {
        private static void Main(string[] args)
        {
            var undirectedCompletedGraph = new UndirectedGraph<string, TaggedUndirectedEdge<string, int>>();

            var edge1 = new TaggedUndirectedEdge<string, int>("A", "B", 2);
            var edge2 = new TaggedUndirectedEdge<string, int>("A", "E", 5);
            var edge3 = new TaggedUndirectedEdge<string, int>("A", "D", 12);
            var edge4 = new TaggedUndirectedEdge<string, int>("B", "C", 4);
            var edge5 = new TaggedUndirectedEdge<string, int>("B", "D", 8);
            var edge6 = new TaggedUndirectedEdge<string, int>("C", "E", 3);
            var edge7 = new TaggedUndirectedEdge<string, int>("C", "D", 3);
            var edge8 = new TaggedUndirectedEdge<string, int>("D", "E", 10);

            undirectedCompletedGraph.AddVerticesAndEdgeRange(
                new List<TaggedUndirectedEdge<string, int>> {edge1, edge2, edge3, edge4, edge5, edge6, edge7, edge8});

            var startVertex = "B";

            var travelingSalesmanResolver = new TravelingSalesmanResolver();
            var travelingVertices =
                travelingSalesmanResolver.ApproximateWith2Approximation(undirectedCompletedGraph, startVertex);

            Console.WriteLine($"Salesman should travel followering vertices: {string.Join(", ", travelingVertices)}");

            Console.ReadLine();
        }
    }
}