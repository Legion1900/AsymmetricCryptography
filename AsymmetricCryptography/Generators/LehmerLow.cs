using System.Numerics;

namespace AsymmetricCryptography.Generators
{
    public class LehmerLow : LehmerGenerator
    {
        public LehmerLow(BigInteger x0) : base(x0)
        {}

        public override byte Next()
        {
            NextXn();
            return Xn.ToByteArray()[0];
        }
    }
}