namespace Complexitytheory.Graph.VertexReachability
{
    public class VertexReachabilityChecker
    {
        public bool CheckReachability(AdjacentMap pGraph, int pMaxSteps, string pStartVertex, string pEndVertex)
        {
            bool isReachable = false;

            if (pMaxSteps == 0)
            {
                isReachable = pGraph[pStartVertex].Contains(pEndVertex) || pStartVertex.Equals(pEndVertex);
            }
            else
            {
                foreach (var nextVertex in pGraph.Keys)
                {
                    isReachable = CheckReachability(pGraph, pMaxSteps - 1, pStartVertex, nextVertex) &&
                                  CheckReachability(pGraph, pMaxSteps - 1, nextVertex, pEndVertex);
                    if (isReachable)
                    {
                        break;
                    }
                }
            }

            return isReachable;
        }
    }
}