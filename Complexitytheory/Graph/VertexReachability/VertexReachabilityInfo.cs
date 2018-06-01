using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complexitytheory.Graph.VertexReachability
{
    public class VertexReachabilityInfo
    {
        public bool IsReachable { get; private set; }

        public List<string> Path { get; private set; }

        public VertexReachabilityInfo(bool pIsReachable, List<string> pPath)
        {
            this.IsReachable = pIsReachable;
            this.Path = pPath;
        }
    }
}
