using System;
using System.Collections;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.Generators.LehmerGenerators
{
    public class LehmerHigh : LehmerGenerator
    {
        public LehmerHigh(uint seed) : base(seed)
        {}

        public override byte[] RandomBytes(int n)
        {
            var output = new byte[n];
            for (int i = 0; i < n; i++)
            {
                var tmp = BitConverter.GetBytes(X);
                output[i] = tmp[tmp.Length - 1];
            }

            return output;
        }

        public override BitArray RandomBits(int n)
        {
            if (n == 0) return new BitArray(0);

            int size = (int)Math.Ceiling((double) n / 8);
            var container = new byte[size];

            for (int i = 0; i < size; i++)
            {
                var tmp = BitConverter.GetBytes(X);
                container[i] = tmp[tmp.Length - 1];
            }

            var output = new BitArray(container);
            
            for (int i = 0; i < size * 8 - n; i++)
            {
                output[i] = false;
            }
            output[size * 8 - n] = true;

            return output;
        }
    }
}