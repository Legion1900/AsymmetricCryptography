using System;
using System.Numerics;

namespace AsymmetricCryptography.Generators.LehmerGenerators
{
    public abstract class LehmerGenerator : IGenerator
    {
        private static readonly BigInteger A = BigInteger.Pow(2, 16) + 1;
        private static readonly BigInteger M = BigInteger.Pow(2, 32);
        private static readonly int C = 119;

        private BigInteger _x;
        
        public abstract byte Next();

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

        protected LehmerGenerator(BigInteger seed)
        {
            X = seed;
        }
    }
}