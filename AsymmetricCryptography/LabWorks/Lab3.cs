using AsymmetricCryptography.Utils;
using System.Diagnostics;

namespace AsymmetricCryptography.LabWorks
{
    public class Lab3
    {
        static void Main()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            double count = 0;
            var n = 32;
            System.Console.WriteLine();
            for (int j = 0; j < 8; j++)
            {
                System.Console.WriteLine($"for {n} bytes");
                for (int i = 0; i < 10; i++)
                {
                    var p = MathI.GenerateBlumPrime(n);
                    count += (double)stopwatch.ElapsedMilliseconds / 1000;
                    stopwatch.Restart();
                }
                
                System.Console.WriteLine($"first method averaged at {count / 10} seconds");

                for (int i = 0; i < 10; i++)
                {
                    var p = MathI.GenerateBlumPrime3(n);
                    count += (double)stopwatch.ElapsedMilliseconds / 1000;
                    stopwatch.Restart();
                }
                
                System.Console.WriteLine($"third method averaged at {count / 10} seconds\n");
                n += 32;
            }
            
        }
    }
}