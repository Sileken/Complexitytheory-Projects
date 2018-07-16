using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Complexitytheory.SetCover
{
    public class SetCoverResolver
    {
        public List<List<string>> ApproximateWithGreedy(List<string> pSet, List<List<string>> pFamily)
        {
            List<List<string>> coveringSets = new List<List<string>>();
            List<string> uncoveredElements = new List<string>(pSet);

            while (uncoveredElements.Count != 0)
            {
                List<string> maxCoveringSet = GetMaxMinCoveringSet(uncoveredElements, pFamily);

                foreach (var coveredElement in maxCoveringSet)
                {
                    if (uncoveredElements.Contains(coveredElement))
                    {
                        uncoveredElements.Remove(coveredElement);
                    }
                }

                coveringSets.Add(maxCoveringSet);
            }

            return coveringSets;
        }

        private List<string> GetMaxMinCoveringSet(List<string> pSet, List<List<string>> pFamily)
        {
            List<string> maxCoveringSet = pFamily[0];
            int coveringElementsCount = maxCoveringSet.Count(pSet.Contains);

            for (int i = 1; i < pFamily.Count; i++)
            {
                List<string> set = pFamily[i];
                int setCoverCount = set.Count(pSet.Contains);

                if (setCoverCount > coveringElementsCount)
                {
                    maxCoveringSet = set;
                    coveringElementsCount = setCoverCount;
                } else if (setCoverCount == coveringElementsCount && set.Count < maxCoveringSet.Count)
                {
                    maxCoveringSet = set;
                }
            }

            return maxCoveringSet;
        }
    }
}
