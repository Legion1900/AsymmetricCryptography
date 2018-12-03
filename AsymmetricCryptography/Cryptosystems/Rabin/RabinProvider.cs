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

        public (Integer y, Integer c1, Integer c2) Encrypt(Integer m, (Integer n, Integer b) publicKey)
        {
            int l = Tools.BitLength(PublicKey.n) / 8;
            int mLength = Tools.BitLength(m) / 8;
            if (mLength > l - 10)
                throw new ArgumentOutOfRangeException(nameof(m), m, $"Message should be no longer than {l - 10} bits");
            var x = FormatMessage(m);
            var y = x * (x + publicKey.b) % publicKey.n;
            int c1 = ((x + publicKey.b / 2) % publicKey.n) % 2;
            int c2 = 
                NumberTheory.IversonBracket(x + publicKey.b / 2, publicKey.n);
        
            return (y, c1, c2);
        }

        private Integer FormatMessage(Integer m)
        {
            int l = Tools.BitLength(PublicKey.n) / 8;
            // 8 bytes = 64 bits
            var r = MathI.GeneratePrime(8);
            Integer tmp = 2;
            r |= tmp.Pow(63);

            System.Console.WriteLine($"length of r: {Tools.BitLength(r)}");

            return 255 * tmp.Pow(8 * (l - 8)) + m * tmp.Pow(64) + r;
        }
    }
}