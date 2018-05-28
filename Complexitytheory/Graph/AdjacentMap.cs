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
    }
}