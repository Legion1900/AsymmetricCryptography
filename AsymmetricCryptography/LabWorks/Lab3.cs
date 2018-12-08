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
            ZKPCracker();
        }

        private static void ZKPCracker()
        {
            System.Console.Write("n: ");
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