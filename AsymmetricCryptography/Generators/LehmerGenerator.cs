using System;
using System.Numerics;

namespace AsymmetricCryptography.Generators
{
    public abstract class LehmerGenerator
    {
        public static readonly BigInteger A = BigInteger.Pow(2, 16) + 1;
        public static readonly BigInteger M = BigInteger.Pow(2, 32);
        public static readonly int C = 119;

        protected BigInteger Xn
        {
            get;
            private set;
        }

        // Should return appropriate byte of Xn
        public abstract byte Next();

        protected LehmerGenerator(BigInteger x0)
        {
            if (x0 <= 0)
                throw new ArgumentOutOfRangeException("BigInteger x0", "Value of x0 should be bigger than 0");
            Xn = x0;
        }

        // Calculates next Xn value
        protected void NextXn()
        {
            Xn = (A * Xn + C) % M;
        }
    }
}