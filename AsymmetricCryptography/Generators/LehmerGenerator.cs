using System;
using System.Numerics;

namespace AsymmetricCryptography.Generators
{
    public abstract class LehmerGenerator
    {
        public static readonly BigInteger A = BigInteger.Pow(2, 16) + 1;
        public static readonly BigInteger M = BigInteger.Pow(2, 32);
        public static readonly int C = 119;

        private BigInteger _x;

        protected BigInteger X
        {
            get
            {
                X = (A * _x + C) % M;
                return _x;
            }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value of X should be bigger than 0");
                _x = value;
            }
        }

        // Should return appropriate byte of Xn
        public abstract byte Next();

        protected LehmerGenerator(BigInteger seed)
        {
            X = seed;
        }
    }
}