using System;
using System.Numerics;
using System.Collections;

namespace AsymmetricCryptography.Generators.LehmerGenerators
{
    public class LehmerLow : LehmerGenerator
    {
        public LehmerLow(uint seed) : base(seed)
        {}
       
        public override byte[] RandomBytes(int n)
        {
            var output = new byte[n];
            for (int i = 0; i < n; i++)
            {
//                output[i] = X.ToByteArray()[0];
                output[i] = BitConverter.GetBytes(X)[0];
            }

            return output;
        }

        public override BitArray RandomBits(int n)
        {
            throw new NotImplementedException();
        }
    }
}