using System;

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
    }
}