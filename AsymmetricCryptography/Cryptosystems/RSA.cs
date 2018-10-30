using System;
using NeinMath;

namespace AsymmetricCryptography.Cryptosystems
{
    public class RSA
    {

        //* This public property is used instead of SendKey method
        public ValueTuple<Integer, Integer> PublicKey
        {
            private set;
            get;
        }

        // private ValueTuple<Integer, Integer> GenerateKeyPair()
        // {

        // }

        // public Integer Encrypt(Integer m)
        // {

        // }

        // public Integer Decrypt(Integer c)
        // {

        // }

        //*? How should Sign signature look like?

        //*? Should it be private? And what parameters should it accept
        // private bool Verify()
        // {

        // }

        // public bool ReceiveKey(ValueTuple<Integer, Integer> publicKey)
        // {

        // }
    }
}