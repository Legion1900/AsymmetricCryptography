using System;
using NeinMath;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.Cryptosystems
{
    public partial class RSAProvider
    {
        // Private key
        private Integer d;

        public (Integer e, Integer n) ExternalPublicKey
        {
            set;
            get;
        }

        //* This public property is used instead of SendKey method
        public (Integer e, Integer n) InternalPublicKey
        {
            private set;
            get;
        }

        public RSAProvider()
        {
            GenerateKeyPair();
        }

        private void GenerateKeyPair()
        {
            Integer p = MathI.GeneratePrime(32),
                q = MathI.GeneratePrime(32),
                n = p * q,
                e = (int)Math.Pow(2, 16) + 1,
                euler = (p - 1) * (q - 1);

            d = e.ModInv(euler);
            InternalPublicKey = (e, n);
        }

        public Integer Encrypt(Integer m, (Integer e, Integer n) publicKey)
        {
            return m.ModPow(publicKey.e, publicKey.n);
        }

        // Integer c -- encrypted message C
        public Integer Decrypt(Integer c)
        {
            return c.ModPow(d, InternalPublicKey.n);
        }


        public (Integer m, Integer s) Sign(Integer m)
        {
            return (m, Encrypt(m, (d, InternalPublicKey.n)));
        }

        //*? Should it be private? And what parameters should it accept
        public bool Verify((Integer m, Integer s) signMessage)
        {
            if (ExternalPublicKey.Equals((0, 0)))
            {
                throw  new InvalidOperationException("ExternalPublicKey has not been set!");
            }

            var m = signMessage.s.ModPow(ExternalPublicKey.e, ExternalPublicKey.n);

            return (signMessage.m == m);
        }
    }
}