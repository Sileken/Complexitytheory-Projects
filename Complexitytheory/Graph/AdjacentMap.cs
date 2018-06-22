using System.Collections.Generic;

namespace Complexitytheory.Graph
{
    public class AdjacentMap : Dictionary<string, List<string>>
    {
        public AdjacentMap() : base()
        { }

        public AdjacentMap(Dictionary<string, List<string>> pAdjacentMap) : base(pAdjacentMap)
        {
        }

        /// <summary>
        /// Get labed edges in format vertex1label|vertex1label
        /// </summary>
        /// <returns></returns>
        public List<string> GetLabedEdges()
        {
            List<string> edges = new List<string>();

            foreach (KeyValuePair<string, List<string>> vertexAdjacentMap in this)
            {
                string vertexName1 = vertexAdjacentMap.Key;
                foreach (string vertexName2 in vertexAdjacentMap.Value)
                {
                    if (!edges.Contains($"{vertexName2}|{vertexName1}"))
                    {
                        edges.Add($"{vertexName1}|{vertexName2}");
                    }
                }
            }

            return edges;
        }
    }
}