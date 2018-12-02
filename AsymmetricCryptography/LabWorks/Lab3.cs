using AsymmetricCryptography.Utils;
using System.Diagnostics;

namespace AsymmetricCryptography.LabWorks
{
    public class Lab3
    {
        private static Stopwatch stopwatch = new Stopwatch();

        static void Main()
        {
            stopwatch.Start();
            for (int j = 0; j < 1000; j++)
            {
                System.Console.WriteLine(MathI.GeneratePrime(32)); 
            }
            System.Console.WriteLine((double)stopwatch.ElapsedMilliseconds / 1000);
        }
    }
}