using System;
using NeinMath;
using AsymmetricCryptography.Utils;
using AsymmetricCryptography.Generators.LehmerGenerators;

namespace AsymmetricCryptography.Cryptosystems.Rabin
{
    public class RabinProvider
    {
        private readonly (Integer p, Integer q) PrivateKey;

        public readonly (Integer n, Integer b) PublicKey;

        public RabinProvider()
        {
            var length = 32;

            PrivateKey.p = MathI.GenerateBlumPrime(length);
            PrivateKey.q = MathI.GenerateBlumPrime(length);
            PublicKey.n = PrivateKey.p * PrivateKey.q;
            PublicKey.b = MathI.RandomI(0, PublicKey.n);
        }

        public (Integer y, bool c1, bool c2) Encrypt(Integer m, (Integer n, Integer b) publicKey)
        {
            int l = Tools.BitLength(publicKey.n) / 8;
            int mLength = Tools.BitLength(m) / 8;
            if (mLength > l - 10)
                throw new ArgumentOutOfRangeException(nameof(m), m, $"Message should be no longer than {l - 10} bits");
            var x = FormatMessage(m);
            var y = x * (x + publicKey.b) % publicKey.n;

            return (y, 
                NumberTheory.C1(x, publicKey.n, publicKey.b),
                NumberTheory.C2(x, publicKey.n, publicKey.b));
        }

        public Integer? Decrypt((Integer y, bool c1, bool c2) encrypted)
        {
            var inversed2 = ((Integer)2).ModInv(PublicKey.n);
            var inversed4 = ((Integer)4).ModInv(PublicKey.n);

            var roots = NumberTheory.QuickSquareRoot(
                encrypted.y + PublicKey.b.ModPow(2, PublicKey.n) * inversed4, PrivateKey);   
            roots[0] = PublicKey.b;

            foreach (var root in roots)
            {
                var x = - PublicKey.b * inversed2 + root;
                if (encrypted.c1 == NumberTheory.C1(x, PublicKey.n, PublicKey.b) 
                && encrypted.c2 == NumberTheory.C2(x, PublicKey.n, PublicKey.b))
                {
                    return InverseFormatMessage(x);
                }
            };

            return null;
        }

        private Integer FormatMessage(Integer m)
        {
            int nLength = Tools.BitLength(PublicKey.n) / 8;
            int mLength = Tools.BitLength(m) / 8;

            if (mLength > nLength - 10 || 2 * mLength < nLength || mLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(m), m, "The message you chose is either too large or < 1.");
            }
            // 8 bytes = 64 bits
            var r = MathI.GeneratePrime(64);

            return 255 * ((Integer)2).Pow(8 * (nLength - 2)) + m * ((Integer)2).Pow(64) + r;
        }

        private Integer InverseFormatMessage(Integer m)
        {
            var tmp = m.ToByteArray();
            var shiftCount = Tools.BitLength(PublicKey.n) / 8 - 2; // = L - 2
            int size = shiftCount - 8 - 2; // 1 | 1 | m | r
            
            if (size < 1 || size >= shiftCount - 8) // since we already subtracted 2
            {
                throw new ArgumentOutOfRangeException("The message formatting was wrong.");
            }

            var k = size - 1;
            var container = new byte[size];
            for(int i = 8; i < 8 + size; i++)
            {
                container[k] = tmp[i];
                k--;
            }
            
            var output = Tools.ToInteger(Tools.ToString(container));
            System.Console.WriteLine($"size: {size} | output: {output.ToHexString()}");

            return output;
        }
    }
}