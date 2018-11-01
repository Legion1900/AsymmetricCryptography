using System;
using System.Diagnostics;
using NeinMath;
using AsymmetricCryptography.Utils;
using AsymmetricCryptography.Cryptosystems;

namespace AsymmetricCryptography.LabWorks
{
    public static class Lab2
    {
        public static (Integer a, Integer b) tuple;
        public static void Main()
        {
            var m = MathI.GenerateStrongPrime(32);
            var rsa  = new RSA();
            rsa.ExternalPublicKey = rsa.InternalPublicKey;
            var signedM = rsa.Sign(m);
            
            System.Console.WriteLine($"(e, n){rsa.InternalPublicKey} \nm: {m} \n(m, s): {signedM}");
            System.Console.WriteLine(rsa.Verify(signedM));
        }
    }
}