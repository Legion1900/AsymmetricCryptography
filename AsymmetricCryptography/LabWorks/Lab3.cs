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
            System.Console.WriteLine("userA");
            var userA = new RabinProvider();
            System.Console.WriteLine("\nuserB");
            var userB = new RabinProvider();

            var seed = (uint)DateTime.Now.Ticks;
            var generator = new LehmerHigh(seed);
            int k = 0;
            
            for (int i = 0; i < 100; i++)
            {
                var message = Tools.ToInteger(
                    generator.RandomBits(8 * 5));

                var encrypted = userA.Encrypt(message, userB.PublicKey);
                var decrypted = userB.Decrypt(encrypted);

                if (message == decrypted)
                {
                    k++;
                }
            }

            System.Console.WriteLine();
            System.Console.WriteLine($"number of rightly decrypted messages = {k}");
            
        }
    }
}