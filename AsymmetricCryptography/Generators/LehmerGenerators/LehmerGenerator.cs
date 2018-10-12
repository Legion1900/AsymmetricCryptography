using System;
using System.Collections;
using System.Numerics;

namespace AsymmetricCryptography.Generators.LehmerGenerators
{
    public abstract class LehmerGenerator : IGenerator
    {
        private static readonly BigInteger A = BigInteger.Pow(2, 16) + 1;
        private static readonly BigInteger M = BigInteger.Pow(2, 32);
        private const int C = 119;

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

        protected LehmerGenerator(BigInteger seed)
        {
            X = seed;

//            Console.WriteLine("Name: {0}", GetType().Name);
//            Console.WriteLine("Seed: {0}", X);
        }

        public abstract byte[] RandomBytes(int n);

        BitArray IGenerator.RandomBits(int n)
        {
            throw new NotImplementedException();
        }
    }
}