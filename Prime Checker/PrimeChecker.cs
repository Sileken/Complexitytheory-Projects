using System;
using Complexitytheory.Prime;
using System.Numerics;
using System.Threading.Tasks;

namespace Prime_Checker
{
    class PrimeChecker
    {
        static void Main(string[] args)
        {
            int iterations = 1000;
            BigInteger[] testPrimes =
            {
                2, 3, 5, 7, 11, 13, 17, 19, 23, 29,
                31, 37, 41, 43, 47, 53, 59, 61, 67, 71,
                104549, 104551, 104561, 104579, 104593, 104597, 104623, 104639, 104651, 104659,
                104677, 104681, 104683, 104693, 104701, 104707, 104711, 104717, 104723, 104729
            };

            long[] compositeNumbers =
            {
                4, 6, 8, 9, 10, 12, 14, 15, 16, 18, 20, 21, 22, 24, 25, 26, 27, 28, 30, 32, 33, 34, 35, 36, 38, 39, 40,
                42, 44, 45, 46, 48, 49, 50, 51, 52, 54, 55, 56, 57, 58, 60, 62, 63, 64, 65, 66, 68, 69, 70, 72, 74, 75,
                76, 77, 78, 80, 81, 82, 84, 85, 86, 87, 88, 90, 91, 92, 93, 94, 95, 96, 98, 99, 100, 102, 104, 105, 106,
                108, 110, 111, 112, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 128, 129, 130, 132,
                133, 134, 135, 136, 138, 140, 141, 142, 143, 144, 145, 146, 147, 148, 150
            };

            Console.WriteLine("Test primes:");
            Parallel.ForEach(testPrimes, testPrime =>
            {
                var primeChecker = new PrimeTester();
                Console.WriteLine(
                    $"Is {testPrime} is a prime: {primeChecker.CheckPrimeBySolovayStrassenTest(testPrime, iterations)}");
            });

            Console.WriteLine("Test composite number:");
            Parallel.ForEach(compositeNumbers, compositeNumber =>
            {
                var primeChecker = new PrimeTester();
                Console.WriteLine(
                    $"Is {compositeNumber} is a composite number: {!primeChecker.CheckPrimeBySolovayStrassenTest(compositeNumber, iterations)}");
                Console.Out.Flush();
            });

            Console.ReadLine();
        }
    }
}
