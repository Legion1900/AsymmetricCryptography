using System;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using NeinMath;
using AsymmetricCryptography.Utils;
using AsymmetricCryptography.Generators.LehmerGenerators;
using AsymmetricCryptography.Cryptosystems.Rabin;

namespace AsymmetricCryptography.LabWorks
{
    public class Lab3
    {
        private static Stopwatch stopwatch = new Stopwatch();

        static void Main()
        {
            Extra();
            // ServerExtra();
            // ServerVerify();
            // ZKPCracker();
        }

        private static void Extra()
        {
            var userA = new RabinProvider();
            var userB = new RabinProvider();
            var m = MathI.RandomI(100, 1000);
            System.Console.WriteLine($"Message to cypher: {m.ToHexString()}");

            var encrypted = userA.Encrypt(m, userB.PublicKey);
            var decrypted = userB.Decrypt(encrypted);
            System.Console.WriteLine($"Encrypted: {encrypted.y.ToHexString()}");
            System.Console.WriteLine($"(c1, c2): ({encrypted.c1}, {encrypted.c2})");
            System.Console.WriteLine($"Decrypted: {decrypted.ToHexString()}\n");

            var signed = userA.Sign(m);
            var verified = userB.Verify(signed, userA.PublicKey);
            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine($"\nSigned: {signed.s}");
            System.Console.WriteLine($"Verification result: {verified}");
        }

        private static void ServerExtra()
        {
            var userA = new RabinProvider();
            var m = MathI.RandomI(100, 1000);
            System.Console.WriteLine($"Message to cypher: {m.ToHexString()}");

            System.Console.Write("Enter server key(n, b): ");
            var n = Tools.ToInteger(Console.ReadLine());
            var b = Tools.ToInteger(Console.ReadLine());
            var key = (n, b);
            var encrypted = userA.Encrypt(m, key);

            System.Console.WriteLine($"Encrypted: {encrypted.y.ToHexString()}");
            System.Console.WriteLine($"(c1, c2): ({encrypted.c1}, {encrypted.c2})");
        }

        private static void ServerVerify()
        {
            var userA = new RabinProvider();
            var m = MathI.RandomI(100, 1000);
            System.Console.WriteLine($"Message to cypher: {m.ToHexString()}");

            var signed = userA.Sign(m);
            System.Console.WriteLine($"PublicKey(n, b): ({userA.PublicKey.n.ToHexString()}, \n{userA.PublicKey.b.ToHexString()})");
            System.Console.WriteLine($"Signed(m, s): ({signed.m.ToHexString()}, \n{signed.s.ToHexString()})");
        }

        private static void ZKPCracker()
        {
            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.Write("\nn: ");
            Integer n = Tools.ToInteger(Console.ReadLine());
            Integer p;
            while (true)
            {
                var t = MathI.RandomI();
                var y = t.ModPow(2, n);
                System.Console.WriteLine($"y: {y.ToHexString()}");
                System.Console.Write("z: ");
                var z = Tools.ToInteger(Console.ReadLine());
                if (y != z || y != -z)
                {
                    p = NumberTheory.GCD(t + z, n);                                            
                    if (p != 1)
                    {
                        break;
                    }
                }       
                System.Console.WriteLine("___________________");                    
            }

            System.Console.WriteLine("\n\nEnd of attack");
            var q = n / p;
            System.Console.WriteLine($"p: {p.ToHexString()}");
            System.Console.WriteLine($"q: {q.ToHexString()}");
            System.Console.WriteLine($"Test n: {(p * q).ToHexString()}");
        }
    }
}