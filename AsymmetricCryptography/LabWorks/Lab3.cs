using System;
using System.Diagnostics;
using System.Collections;
using NeinMath;
using AsymmetricCryptography.Utils;
using AsymmetricCryptography.Generators.LehmerGenerators;
using AsymmetricCryptography.Cryptosystems.Rabin;

namespace AsymmetricCryptography.LabWorks
{
    public class Lab3
    {
        static void Main()
        {
            RabinProvider a = new RabinProvider();
            RabinProvider b = new RabinProvider();
            var m = MathI.RandomI(1000, 1000000);
            var sign = a.Sign(m);
            System.Console.WriteLine(b.Verify(sign, a.PublicKey));
        }
    }
}