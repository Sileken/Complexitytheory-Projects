using System.Collections.Generic;
using Complexitytheory.SubsetSum;

namespace Complexitytheory.SAT.ReductionInfos
{
    public class ThreeSatToSubsetSumReductionInfo
    {
        public Dictionary<double, string> NumberVariableMapping { get; private set; }

        public SubSetSumInfo SubSetSumInfo { get; private set; }

        public int VariableCount { get; private set; }

        public int ClausesCount { get; private set; }

        public ThreeSatToSubsetSumReductionInfo(Dictionary<double, string> pNumberVariableMapping,
            SubSetSumInfo pSubSetSumInfo, int pVariableCount, int pClausesCount)
        {
            this.NumberVariableMapping = pNumberVariableMapping;
            this.SubSetSumInfo = pSubSetSumInfo;
            this.VariableCount = pVariableCount;
            this.ClausesCount = pClausesCount;
        }
    }
}