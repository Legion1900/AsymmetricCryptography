using System;
using NeinMath;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.Cryptosystems
{
    public class RSA
    {
        // Private key
        private Integer d;

        private (Integer e, Integer n) externalPublicKey;

        //* This public property is used instead of SendKey method
        public (Integer e, Integer n) PublicKey
        {
            private set;
            get;
        }

        public RSA(Integer p, Integer q, Integer e)
        {
            if (!PrimalityTests.MillerRabin(p) || !PrimalityTests.MillerRabin(q))
                throw new ArgumentException("p and q must be prime numbers");

            var n = p * q;
            var euler = (p - 1) * (q - 1);
            if (MathI.GCD(e, euler) != 1)
                throw new ArgumentException("Euler(p * q) should be mutually simple with e");
            d = e.ModInv(euler);
            PublicKey = new ValueTuple<Integer, Integer>(e, n);
        }

        // public bool ReceiveKey((Integer e, Integer n) publicKey)
        // {

        // }

        // private ValueTuple<Integer, Integer> GenerateKeyPair()
        // {

        // }

        public Integer Encrypt(Integer m, (Integer e, Integer n) publicKey)
        {
            return m.ModPow(publicKey.e, publicKey.n);
        }

        // Integer c -- encrypted message C
        public Integer Decrypt(Integer c)
        {
            return c.ModPow(d, PublicKey.n);
        }

        //*? How should Sign signature look like?

        //*? Should it be private? And what parameters should it accept
        // private bool Verify()
        // {

        // }
    }
}