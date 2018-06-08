using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Complexitytheory.Median;

namespace Median_Finder
{
    class MedianFinder
    {
        static void Main(string[] args)
        {
            var numbersCount = 100000000;
            var randomGen = new Random();
            int[] numbers = new int[numbersCount];

            for (var i = 0; i < numbersCount; i++)
            {
                numbers[i] = randomGen.Next();
            }

            //Console.WriteLine($"Try to find media in {string.Join(", ", numbers)}");

            Complexitytheory.Median.MedianFinder medianFinder = new Complexitytheory.Median.MedianFinder();
            int kMedia = Convert.ToInt32(Math.Ceiling(numbers.Length / 2.0));

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var foundBPPMedian = medianFinder.FindBPPKMedian(kMedia, numbers);

            stopwatch.Stop();

            Console.WriteLine($"{Console.Out.NewLine}BPP Median {foundBPPMedian}");
            Console.WriteLine($"Time elapsed to find median with bbp: {stopwatch.Elapsed}");

            stopwatch = new Stopwatch();
            stopwatch.Start();

            var foundSortMedia = medianFinder.FindSortKMedian(kMedia, numbers);

            stopwatch.Stop();

            Console.WriteLine($"{Console.Out.NewLine}Sort Median {foundSortMedia}");
            Console.WriteLine($"Time elapsed to find median with sort: {stopwatch.Elapsed}");

            Console.ReadLine();
        }
    }
}
