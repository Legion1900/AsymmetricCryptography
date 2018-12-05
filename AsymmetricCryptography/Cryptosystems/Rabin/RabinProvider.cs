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
            var length = 64;

            PrivateKey.p = MathI.GenerateBlumPrime(length);
            PrivateKey.q = MathI.GenerateBlumPrime(length);
            PublicKey.n = PrivateKey.p * PrivateKey.q;
            PublicKey.b = MathI.RandomI(0, PublicKey.n);

            System.Console.WriteLine($"private key " + 
            $"(p = {Tools.ByteLength(PrivateKey.q)} bytes, " + 
            $"q = {Tools.ByteLength(PrivateKey.q)} bytes)): " +
            $"({PrivateKey.p.ToHexString()}, {PrivateKey.q.ToHexString()})");

            System.Console.WriteLine($"public key " + 
            $"(n = {Tools.ByteLength(PublicKey.n)} bytes, " + 
            $"b = {Tools.ByteLength(PublicKey.b)} bytes)): " +
            $"({PublicKey.n.ToHexString()}, {PublicKey.b.ToHexString()})");

        }

        public (Integer y, bool c1, bool c2) Encrypt(Integer m, (Integer n, Integer b) publicKey)
        {
            int l = Tools.BitLength(publicKey.n) / 8;
            int mLength = Tools.BitLength(m) / 8;
            if (mLength > l - 10)
                throw new ArgumentOutOfRangeException(nameof(m), m, $"Message should be no longer than " + 
                $"{Tools.ByteLength(publicKey.n)} - {10} bytes");
            var x = FormatMessage(m, publicKey);
            var y = (x * (x + publicKey.b)) % publicKey.n;

            return (y, 
                NumberTheory.C1(x, publicKey.n, publicKey.b),
                NumberTheory.C2(x, publicKey.n, publicKey.b));
        }

        public Integer Decrypt((Integer y, bool c1, bool c2) encrypted)
        {
            var inv2 = ((Integer)2).ModInv(PublicKey.n);
            var inv4 = ((Integer)4).ModInv(PublicKey.n);

            var bPow2 = PublicKey.b.ModPow(2, PublicKey.n);

            var roots = NumberTheory.QuickSquareRoot(
                NumberTheory.Mod(encrypted.y + bPow2 * inv4, PublicKey.n),
                PrivateKey);

            foreach (var root in roots)
            {
                var x = NumberTheory.Mod(-PublicKey.b * inv2 + root, PublicKey.n);
                if (encrypted.c1 == NumberTheory.C1(x, PublicKey.n, PublicKey.b) 
                && encrypted.c2 == NumberTheory.C2(x, PublicKey.n, PublicKey.b))
                {
                    return InverseFormatMessage(x);
                }
            };

            throw new Exception("Decrypt failed, c1 || c2 didn't coincide.");
        }

        private Integer FormatMessage(Integer m, (Integer n, Integer b) publicKey)
        {
            int nLength = Tools.BitLength(publicKey.n) / 8; 
            int mLength = Tools.BitLength(m) / 8;

            if (mLength > nLength - 10 || 2 * mLength > nLength || mLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(m), m, "The message you chose is either too large or too small." + 
                $"\n{mLength} >? {nLength - 10}   |   {2 * mLength} <? {nLength}");
            }
            // 8 bytes = 64 bits
            var r = MathI.GeneratePrime(64);
            var output = 255 * ((Integer)2).Pow(8 * (nLength - 2)) + m * ((Integer)2).Pow(64) + r;

            return output;
        }

        public Integer InverseFormatMessage(Integer m)
        {
            var tmp = m.ToByteArray();
            var nLength = Tools.BitLength(PublicKey.n) / 8;
            var shiftCount = nLength - 2;
            int mLength = shiftCount - 8 - 2; // 1 | 1 | m | r
            
            if (mLength > nLength - 10 || mLength < 1) // since we already subtracted 2
            {
                throw new ArgumentOutOfRangeException("The message formatting was wrong.");
            }

            var k = mLength - 1;
            var container = new byte[mLength];
            for(int i = 8; i < 8 + mLength; i++)
            {
                container[k] = tmp[i];
                k--;
            }
            
            var output = Tools.ToInteger(Tools.ToString(container));

            return output;
        }
    }
}