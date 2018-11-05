using NeinMath;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.Utils
{
    public static class PrimalityTests
    {
        public static bool MillerRabin(Integer p)
        {
            string[] primesStr = System.IO.File.ReadAllText("./Generated/primes.txt").Split(' ');
            for (int i = 0; i < 100; i++)
            {
                if (p % int.Parse(primesStr[i]) == 0) return false;
            }


            Integer d = p - 1, modPow, x;
            int s, k = 100; // Pow(1/4, k) = probability of p being pseudo-prime
            bool check = false;

            for (s = 0; s < (p - 1).ToByteArray().Length; s++)
            {
                if (d % 2 == 0) d /= 2;
                else break;
            }
            s++;

            while (k != 0)
            {
                x = MathI.RandomI(1, p);
                if (MathI.GCD(x, p) != 1) return false;

                modPow = x.ModPow(d, p);
                if (modPow == 1 || modPow == p - 1)
                {
                    k--;
                    continue;
                }

                x = x.ModPow(d, p);
                for (int i = 0; i < s; i++)
                {
                    x = x.ModPow(2, p);
                    if (x == p - 1)
                    {
                        check = true;
                        break;
                    }
                    else if (x == 1) return false;
                }

                if (!check) return false;
                k--;
            }

            return true;
        }
    }
}