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

        // *! Fix list of parameters according fo class fields
        public Integer Encrypt(Integer m, ValueTuple<Integer, Integer> publicKey)
        {
            System.Console.WriteLine("{0} ^ {1} mod {2}", m, publicKey.Item1, publicKey.Item2);
            return m.ModPow(publicKey.Item1, publicKey.Item2);
        }

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