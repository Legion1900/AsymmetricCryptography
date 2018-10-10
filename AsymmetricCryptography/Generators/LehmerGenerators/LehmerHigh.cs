using System.Numerics;

namespace AsymmetricCryptography.Generators.LehmerGenerators
{
    public class LehmerHigh : LehmerGenerator
    {
        public LehmerHigh(BigInteger seed) : base(seed)
        {}

        public override byte NextByte()
        {
            var bytes = X.ToByteArray();
            return bytes[bytes.Length - 1] != 0 ? bytes[bytes.Length - 1] : bytes[bytes.Length - 2];
        }
    }
}