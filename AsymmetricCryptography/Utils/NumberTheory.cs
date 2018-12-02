using NeinMath;
using System;

namespace AsymmetricCryptography.Utils
{
    public class NumberTheory
    {
        public static Integer GCD(Integer a, Integer b)
        {
            if (b == 0)
                return a;
            else
                return GCD(b, a % b);
        }

        // ax + by = g = gcd(a, b)
        public static (Integer g, Integer u, Integer v) ExtendedGCD(Integer a, Integer b)
        {
            if (a == 0)
                return (b, 0, 1);
            else
            {
                var egcd = ExtendedGCD(b % a, a);
                return (egcd.g, egcd.v - (b / a) * egcd.u, egcd.u);
            }
        }

        public static Integer[] QuickSquareRoot(Integer y, (Integer p, Integer q) n)
        {
            var s1 = y.ModPow((n.p + 1) / 4, n.p);
            var s2 = y.ModPow((n.q + 1) / 4, n.q);
            var mod = n.p * n.q;

            var egcd = ExtendedGCD(n.p, n.q);

            return new Integer[]{
                (egcd.u * n.p * s1 + egcd.v * n.p * s2) % mod,
                (egcd.u * n.p * s1 - egcd.v * n.p * s2) % mod + mod,
                (-egcd.u * n.p * s1 + egcd.v * n.p * s2) % mod,
                (-egcd.u * n.p * s1 - egcd.v * n.p * s2) % mod + mod
            };
        }
        
        public static Integer JacobiSymbol(Integer a, Integer b)
        {
            // Mutual simlicity check
            if (GCD(a, b) != 1)
            {
                return 0;
            }

            // Transition to positive numbers
            int ans = 1;
            if (a < 0)
            {
                a = -a;
                if (b % 4 == 3)
                    ans = -ans;
            }

            // Getting rid of parity
            int t;
            while (true)
            {
                t = 0;
                while (a % 2 == 0)
                {
                    t++;
                    a /= 2;
                }
                if (t % 2 == 1)
                    if (b % 8 == 3 || b % 8 == 5)
                        ans = -ans;

                // Quadratic reciprocity
                if ((a % 4 == 3) && (b % 4 == 3))
                    ans = -ans;
                var tmp = a;
                a = b % tmp;
                b = tmp;

                if (a == 0)
                    break;
            }

            return ans;
        }
    }
}