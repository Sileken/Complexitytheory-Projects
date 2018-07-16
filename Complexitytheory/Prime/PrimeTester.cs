using System;
using System.Numerics;

namespace Complexitytheory.Prime
{
    public class PrimeTester
    {
        readonly Random _random = new Random();

        public bool CheckPrimeBySolovayStrassenTest(BigInteger pNumber, BigInteger pIteration)
        {
            if (pNumber < 2)
            {
                return false;
            }

            if (pNumber != 2 && pNumber % 2 == 0)
            {
                return false;
            }

            // pNumber is odd
            for (var i = 0; i < pIteration; i++)
            {
                BigInteger randomNumber = _random.Next() % (pNumber - 1) + 1;

                var gcd = GCD(randomNumber, pNumber);
                if (gcd > 1)
                {
                    return false;
                }

                BigInteger jacobiSymbol = (pNumber + JacobiSymbol(randomNumber, pNumber)) % pNumber;
                BigInteger mod = BigInteger.Pow(randomNumber, (int) ((pNumber - 1) / 2)) % pNumber;

                if (jacobiSymbol != 0 && jacobiSymbol != mod)
                {
                    return false;
                }
            }

            return true;
        }

        private BigInteger GCD(BigInteger a, BigInteger b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            if (a == 0)
                return b;
            else
                return a;
        }

        //Calculating Jacobi symbol. This gives the following algorithm for finding (a|n). Suppose n is odd and 0 < a < n:
        private int JacobiSymbol(BigInteger pA, BigInteger pN)
        {
            var a = pA;
            var n = pN;
            var t = 1;

            if (a < 0)
            {
                a = -a;
                if (n % 4 == 3)
                {
                    t = -t;
                }
            }

            if (a == 1)
            {
                return t;
            }

            while (a != 0)
            {
                if (a < 0)
                {
                    a = -a;
                    if (n % 4 == 3)
                    {
                        t = -t;
                    }
                }
                
                while (a % 2 == 0)
                {
                    a = a / 2;
                    if (n % 8 == 3 || n % 8 == 5)
                    {
                        t = -t;
                    }
                }

                var tempN = n;
                n = a;
                a = tempN;

                if (a % 4 == 3 && n % 4 == 3)
                {
                    t = -t;
                }

                a = a % n;

                if (a > n / 2)
                {
                    a = a - n;
                }
            }

            return n == 1 ? t : 0;
        }
    }
}
