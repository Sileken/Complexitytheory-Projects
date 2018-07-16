using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Complexitytheory.Median
{
    public class MedianFinder
    {
        private readonly Random _randomGenerator = new Random();

        public int FindBPPKMedian(int pKMedian, int[] pNumbers)
        {
            int rndmMedian = pNumbers[_randomGenerator.Next(0, pNumbers.Length - 1)];
            int count = pNumbers.Count(n => n <= rndmMedian);
            int median = count == pKMedian
                ? rndmMedian
                : count > pKMedian
                    ? FindBPPKMedian(pKMedian, pNumbers.Where(n => n <= rndmMedian).ToArray())
                    : FindBPPKMedian(pKMedian - count, pNumbers.Where(n => n > rndmMedian).ToArray());

            return median;
        }

        public int FindSortKMedian(int pKMedia, int[] pNumbers)
        {
            if (pKMedia > pNumbers.Length)
            {
                throw new Exception("KMedia musst be greater then Numbers.Count.");
            }

            Array.Sort(pNumbers);

            return pNumbers[pKMedia-1];
        }
    }
}