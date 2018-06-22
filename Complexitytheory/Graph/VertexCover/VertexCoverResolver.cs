using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexitytheory.Graph.VertexCover
{
    public class VertexCoverResolver
    {
        private readonly Random _rnd;

        public VertexCoverResolver()
        {
            _rnd = new Random(Environment.TickCount);
        }

        public List<String> ApproximateMinVertexCover(AdjacentMap pUndirectedGraph)
        {
            List<string> coveredVertice = new List<string>();
            List<string> labedEdges = pUndirectedGraph.GetLabedEdges();

            while (labedEdges.Count != 0)
            {
                int rndEdgeIndex = _rnd.Next() % labedEdges.Count;
                var labedEdge = labedEdges[rndEdgeIndex];
                string[] vertexes = labedEdge.Split('|');

                foreach (var vertex in vertexes)
                {
                    if (!coveredVertice.Contains(vertex))
                    {
                        coveredVertice.Add(vertex);
                    }
                }

                foreach (string vertex2 in pUndirectedGraph[vertexes[0]])
                {
                    string edgeLable = $"{vertexes[0]}|{vertex2}";
                    string edgeLabeReverse = $"{vertex2}|{vertexes[0]}";
                    if (labedEdges.Contains(edgeLable))
                    {
                        labedEdges.Remove(edgeLable);
                    }
                    else if(labedEdges.Contains(edgeLabeReverse))
                    {
                        labedEdges.Remove(edgeLabeReverse);
                    }
                }
                foreach (string vertex2 in pUndirectedGraph[vertexes[1]])
                {
                    string edgeLable = $"{vertexes[1]}|{vertex2}";
                    string edgeLabeReverse = $"{vertex2}|{vertexes[0]}";
                    if (labedEdges.Contains(edgeLable))
                    {
                        labedEdges.Remove(edgeLable);
                    }
                    else if (labedEdges.Contains(edgeLabeReverse))
                    {
                        labedEdges.Remove(edgeLabeReverse);
                    }
                }
            }

            return coveredVertice;
        }
    }
}
