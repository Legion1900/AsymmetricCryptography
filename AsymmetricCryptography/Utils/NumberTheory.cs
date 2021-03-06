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

        public static Integer Mod(Integer x, Integer n)
        {
            x %= n;
            return x < 0? x + n: x;
        }

        public static Integer[] QuickSquareRoot(Integer y, (Integer P, Integer Q) n)
        {

            var mP = y.ModPow((n.P + 1) / 4, n.P);
            var mQ = y.ModPow((n.Q + 1) / 4, n.Q);
            var N = n.P * n.Q;

            var egcd = ExtendedGCD(n.P, n.Q);
            
            var output = new Integer[4];

            output[0] = Mod(egcd.u * n.P * mQ + egcd.v * n.Q * mP, N);
            output[1] = Mod(egcd.u * n.P * mQ - egcd.v * n.Q * mP, N);
            output[2] = Mod(-egcd.u * n.P * mQ + egcd.v * n.Q * mP, N);
            output[3] = Mod(-egcd.u * n.P * mQ - egcd.v * n.Q * mP, N);

            return output;
        }
        
        public static int JacobiSymbol(Integer a, Integer b)
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

        public static bool IversonBracket(Integer a, Integer b)
        {
            return JacobiSymbol(a, b) == 1;
        }

        public static bool C1 (Integer x, Integer n, Integer b)
        {
            return ((x + b * ((Integer)2).ModInv(n)) % n) % 2 == 1;
        }

        public static bool C2 (Integer x, Integer n, Integer b)
        {
            return NumberTheory.IversonBracket(x + b * ((Integer)2).ModInv(n), n);
        }
    }
}