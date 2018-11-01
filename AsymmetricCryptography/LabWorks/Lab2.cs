using System;
using System.Diagnostics;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.LabWorks
{
    public static class Lab2
    {
        public static void Main()
        {
            // MathI.GeneratePrimes(500);
            var stopwatch = new Stopwatch();
            double a, b = 0;
            for(int j = 1; j < 11; j++)
            {
                a = 0;
                for (int i = 0; i < 20; i++)
                {
                    stopwatch.Restart();
                    MathI.GeneratePrime(32);
                    a += (double)stopwatch.ElapsedMilliseconds / 1000;
                }
                System.Console.WriteLine(j + " instance took " + a / 10 + " seconds to generate.");
                b += a;
            }
            System.Console.WriteLine();
        }
    }
}