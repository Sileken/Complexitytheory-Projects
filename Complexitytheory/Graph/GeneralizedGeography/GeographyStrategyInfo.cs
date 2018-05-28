using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexitytheory.Graph.GeneralizedGeography
{
    public class GeographyStrategyInfo
    {
        public bool FoundStrategy { get; private set; }

        public List<List<string>> Strategies { get; private set; }

        public GeographyStrategyInfo(bool pFoundStrategy, List<List<string>> pStrategies)
        {
            this.FoundStrategy = pFoundStrategy;
            this.Strategies = pStrategies;
        }
    }
}
