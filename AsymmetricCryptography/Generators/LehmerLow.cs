using System.Numerics;

namespace AsymmetricCryptography.Generators
{
    public class LehmerLow : LehmerGenerator
    {
        public LehmerLow(BigInteger seed) : base(seed)
        {}

        public override byte Next()
        {
            return X.ToByteArray()[0];
        }
    }
}