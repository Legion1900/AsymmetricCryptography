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
            var message = Tools.ToInteger(
                generator.RandomBits(8 * 4));

            
            var encrypted = userA.Encrypt(message, userB.PublicKey);
            // System.Console.WriteLine($"encrypted: {encrypted}\n");
            
            var decrypted = userB.Decrypt(encrypted);
            if (decrypted != null)
            {
                // System.Console.WriteLine($"\ndecrypted: {decrypted} | length: {Tools.ByteLength((Integer)decrypted)}");
            }
            else 
            {
                System.Console.WriteLine("wut");
            }
            
        }
    }
}