using System;
using System.Collections;
using System.Numerics;

namespace AsymmetricCryptography.Generators.LehmerGenerators
{
    public abstract class LehmerGenerator : IGenerator
    {
        private static readonly uint A = (uint)Math.Pow(2, 16) + 1;
        private static readonly long M = (long)Math.Pow(2, 32);
        private const int C = 119;

        private uint _x;

        public string Seed { get; }

        protected uint X
        {
            get
            {
                uint tmp = _x;
                Next();
                return tmp;
            }
            private set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Value of X should be bigger than 0");
                _x = value;
            }
        }

        protected LehmerGenerator(uint seed)
        {
            X = seed;
            Seed = seed.ToString();
        }

        public abstract byte[] RandomBytes(int n);
        public abstract BitArray RandomBits(int n);

        private void Next()
        {
            X = (uint)((A * _x + C) % M);
        }

        // BitArray IGenerator.RandomBits(int n)
        // {
        //     throw new NotImplementedException();
        // }
    }
}