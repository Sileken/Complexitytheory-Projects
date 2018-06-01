using System.Collections.Generic;

namespace Complexitytheory.Graph.VertexReachability
{
    public class VertexReachabilityChecker
    {
        public VertexReachabilityInfo CheckReachability(AdjacentMap pGraph, int pMaxSteps, string pStartVertex,
            string pEndVertex)
        {
            VertexReachabilityInfo checkReachabilityIntern = CheckReachabilityIntern(pGraph, pMaxSteps, pStartVertex, pEndVertex);

            if (checkReachabilityIntern.IsReachable)
            {
                if (checkReachabilityIntern.Path.Count > 0 && checkReachabilityIntern.Path[0] != pStartVertex)
                {
                    checkReachabilityIntern.Path.Insert(0, pStartVertex);
                }

                if (checkReachabilityIntern.Path[checkReachabilityIntern.Path.Count - 1] != pEndVertex)
                {
                    checkReachabilityIntern.Path.Add(pEndVertex);
                }
            }

            return checkReachabilityIntern;
        }

        private VertexReachabilityInfo CheckReachabilityIntern(AdjacentMap pGraph, int pMaxSteps, string pStartVertex, string pEndVertex)
        {
            bool isReachable = false;
            List<string> tempPath = new List<string>();

            if (pMaxSteps == 0)
            {
                isReachable = pGraph[pStartVertex].Contains(pEndVertex) || pStartVertex.Equals(pEndVertex);
            }
            else
            {
                foreach (var nextVertex in pGraph.Keys)
                {
                    VertexReachabilityInfo isReachablePath1 = CheckReachabilityIntern(pGraph, pMaxSteps - 1, pStartVertex, nextVertex);
                    VertexReachabilityInfo isReachablePath2 = CheckReachabilityIntern(pGraph, pMaxSteps - 1, nextVertex, pEndVertex);

                    isReachable = isReachablePath1.IsReachable && isReachablePath2.IsReachable;

                    if (isReachable)
                    {
                        tempPath.Add(nextVertex);
                        tempPath.AddRange(isReachablePath2.Path);
                        break;
                    }
                }
            }

            return new VertexReachabilityInfo(isReachable, tempPath);
        }
    }
}