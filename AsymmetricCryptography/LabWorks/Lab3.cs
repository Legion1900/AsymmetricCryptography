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
        private static Stopwatch stopwatch = new Stopwatch();
        static void Main()
        {
            var userA = new RabinProvider();
            var userB = new RabinProvider();

            var seed = (uint)DateTime.Now.Ticks;
            var generator = new LehmerHigh(seed);
            double n = 1000;


            int k = 0, j = 32;
            System.Console.WriteLine($"message length: {j * 8} bytes");
            System.Console.WriteLine($"user A modulus length: {Tools.BitLength(userA.PublicKey.n)}");
            System.Console.WriteLine($"user A modulus length: {Tools.BitLength(userB.PublicKey.n)}");
            for (double i = 1; i < n+1; i++)
            {
                var message = Tools.ToInteger(
                    generator.RandomBits(8 * j));

                var encrypted = userA.Encrypt(message, userB.PublicKey);
                var decrypted = userB.Decrypt(encrypted);

                
                if (message == decrypted)
                {
                    k++;
                }
                Console.Write($"\r{(int)(i / n * 100)}% | {k}/{n} decrypted rightly");
            }
            System.Console.WriteLine();
        }
    }
}