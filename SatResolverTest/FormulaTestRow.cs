using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.SAT;

namespace SatResolverTest
{
    public class FormulaTestRow
    {
        public Formula Formula { get; set; }
        public bool ExpectedSatisfiability { get; set; }
    }
}
