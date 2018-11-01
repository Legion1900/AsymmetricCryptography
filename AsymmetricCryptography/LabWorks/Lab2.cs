using System;
using System.Diagnostics;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.LabWorks
{
    public static class Lab2
    {
        public static void Main()
        {
            var stopwatch = new Stopwatch();
            for(int i = 0; i < 40; i++)
            {
                stopwatch.Restart();
                MathI.GeneratePrime(32);
                System.Console.WriteLine((double)stopwatch.ElapsedMilliseconds / 1000 + " seconds");
            }
        }
    }
}