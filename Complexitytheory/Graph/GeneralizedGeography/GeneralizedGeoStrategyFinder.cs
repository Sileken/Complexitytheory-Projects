using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexitytheory.Graph.GeneralizedGeography
{
    public class GeneralizedGeoStrategyFinder
    {
        public static GeographyStrategyInfo FindStrategy(AdjacentMap pDirectedGraph, string pStartNode)
        {
            GeographyStrategyInfo geographyStrategyInfo = FindStrategy(pDirectedGraph, pStartNode, true);

            return geographyStrategyInfo.FoundStrategy
                ? geographyStrategyInfo
                : new GeographyStrategyInfo(false, new List<List<string>>());
        }


        private static GeographyStrategyInfo FindStrategy(AdjacentMap pDirectedGraph, string pStartNode, bool pPlayer1Round)
        {
            GeographyStrategyInfo strategyInfo;
            //1. Measure the out-degree of node nstart.If this degree is 0, then return reject, because there are no moves available for player one.
            int nodeOutDegree = pDirectedGraph[pStartNode].Count;
            if (nodeOutDegree == 0)
            {
                // no moves possible
                strategyInfo = new GeographyStrategyInfo(false, new List<List<string>>() { new List<string>(){ pStartNode }  });
            }
            else
            {
                //2. Construct a list of all nodes reachable from nstart by one edge: n1, n2, ..., ni.
                List<string> nextReachableNodes = pDirectedGraph[pStartNode];
                //3. Remove nstart and all edges connected to it from G to form G1.
                AdjacentMap graph1 = new AdjacentMap(pDirectedGraph);
                graph1.Remove(pStartNode);
                List<string> keys = new List<string>(graph1.Keys);
                foreach (string key in keys)
                {
                    List<string> neighbors = graph1[key];
                    if (neighbors.Contains(pStartNode))
                    {
                        List<string> newNeighbors = neighbors.ToList();
                        newNeighbors.Remove(pStartNode);
                        graph1[key] = newNeighbors;
                    }
                }

                //4. For each node nj in the list n1, ..., ni, call M(< G1, nj >).
                bool foundStrategy = true;
                List<List<string>> tempStrategies = new List<List<string>>();
                foreach (string nextReachableNode in nextReachableNodes)
                {
                    var currentNodeStrategyInfo = FindStrategy(graph1, nextReachableNode, !pPlayer1Round);

                    foundStrategy = foundStrategy && currentNodeStrategyInfo.FoundStrategy;
                    if (!currentNodeStrategyInfo.FoundStrategy && pPlayer1Round || currentNodeStrategyInfo.FoundStrategy && !pPlayer1Round )
                    {
                        tempStrategies.AddRange(currentNodeStrategyInfo.Strategies);                        
                    }
                }

                foreach (var strategy in tempStrategies)
                {
                    strategy.Insert(0, pStartNode);
                }
                
                //5. If all of these calls return accept, then no matter which decision P1 makes, P2 has a strategy to win, so return reject. Otherwise(if one of the calls returns reject) P1 has a choice that will deny any successful strategies for P2, so return accept.
                strategyInfo = new GeographyStrategyInfo(!foundStrategy, tempStrategies);
            }

            return strategyInfo;
        }
    }
}
