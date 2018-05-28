using System;
using System.Collections.Generic;
using System.Linq;

namespace Complexitytheory.SubsetSum
{
    public class SubsetSumResolver
    {
        public int GeneratedNodesCount { get; private set; } = 0;

        public List<double[]> ResolveSubsetSum(SubSetSumInfo pSubSetSumInfo)
        {
            GeneratedNodesCount = 0;

            List<double[]> resolvedSubSets = new List<double[]>();
            double[] weights = (double[])pSubSetSumInfo.Weights.Clone();
            double targetSum = pSubSetSumInfo.TargetSum;

            Array.Sort(weights);
            double[] sumVector = new double[weights.Length];

            double tempSum = weights.Sum();

            if (sumVector[0] <= targetSum && tempSum >= targetSum)
            {
                SubsetSum(weights, sumVector, 0, 0, 0, targetSum, resolvedSubSets);
            }

            return resolvedSubSets;
        }

        private void SubsetSum(double[] pWeights, double[] pSumVector, int pSumVectorRange, double pLastSum, int pTreeDepth, double pTargetSum, List<double[]> pResolvedSubSets)
        {
            GeneratedNodesCount++;

            if (pTargetSum.Equals(pLastSum))
            {
                pResolvedSubSets.Add(pSumVector.Take(pSumVectorRange).ToArray());

                if (pTreeDepth + 1 < pWeights.Length && pLastSum - pWeights[pTreeDepth] + pWeights[pTreeDepth + 1] <= pTargetSum)
                {
                    // Exclude previous added item and consider next candidate
                    SubsetSum(pWeights, pSumVector, pSumVectorRange - 1, pLastSum - pWeights[pTreeDepth], pTreeDepth + 1, pTargetSum, pResolvedSubSets);
                }
            }
            else
            {
                if (pTreeDepth < pWeights.Length && pLastSum + pWeights[pTreeDepth] <= pTargetSum)
                {
                    // generate nodes along the breadth
                    for (int i = pTreeDepth; i < pWeights.Length; i++)
                    {
                        pSumVector[pSumVectorRange] = pWeights[i];

                        if (pLastSum + pWeights[i] <= pTargetSum)
                        {
                            // consider next level node (along depth)
                            SubsetSum(pWeights, pSumVector, pSumVectorRange + 1, pLastSum + pWeights[i], i + 1, pTargetSum, pResolvedSubSets);
                        }
                    }
                }
            }
        }
    }
}
