using System.Numerics;

namespace AsymmetricCryptography.Generators.LehmerGenerators
{
    public class LehmerLow : LehmerGenerator
    {
        public LehmerLow(BigInteger seed) : base(seed)
        {}

//        public override byte[] RandomBytes(int n)
//        {
//            return X.ToByteArray()[0];
//        }
       
        public override byte[] RandomBytes(int n)
        {
            var output = new byte[n];
            for (int i = 0; i < n; i++)
            {
                output[i] = X.ToByteArray()[0];
            }

            return output;
        }
    }
}