using System.Numerics;

namespace AsymmetricCryptography.Generators.LehmerGenerators
{
    public class LehmerHigh : LehmerGenerator
    {
        public LehmerHigh(BigInteger seed) : base(seed)
        {}

        public override byte[] RandomBytes(int n)
        {
            var output = new byte[n];
            for (int i = 0; i < n; i++)
            {
                var tmp = X.ToByteArray();
                if (tmp[tmp.Length - 1] != 0)
                    output[i] = tmp[tmp.Length - 1];
                else output[i] = tmp[tmp.Length - 2];
            }

            return output;
        }
    }
}