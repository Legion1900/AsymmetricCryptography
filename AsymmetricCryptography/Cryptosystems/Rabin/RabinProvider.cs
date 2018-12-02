using System;
using NeinMath;
using AsymmetricCryptography.Utils;
using AsymmetricCryptography.Generators.LehmerGenerators;

namespace AsymmetricCryptography.Cryptosystems.Rabin
{
    public class RabinProvider
    {

        private (Integer p, Integer q) privateKey;

        public readonly (Integer n, Integer b) PublicKey;

        public RabinProvider()
        {
            privateKey.p = MathI.GenerateBlumPrime(32);
            privateKey.q = MathI.GenerateBlumPrime(32);
            PublicKey.n = privateKey.p * privateKey.q;
            PublicKey.b = MathI.RandomI(0, PublicKey.n);
        }

        public Integer Encrypt(Integer m)
        {
            var x = FormatMessage(m);
            var y = x * (x + PublicKey.b) % PublicKey.n;
        }

        private Integer FormatMessage(Integer m)
        {
            int l = Tools.BitLength(PublicKey.n);
            // 8 bytes = 64 bits
            var r = MathI.GeneratePrime(8);
            return 255 * (int)Math.Pow(2, 8 * (l - 8)) + m * (int)Math.Pow(2, 64) + r;
        }
    }
}