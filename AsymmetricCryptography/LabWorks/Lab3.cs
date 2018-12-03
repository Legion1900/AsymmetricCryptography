using System;
using System.Diagnostics;
using System.Collections;
using NeinMath;
using AsymmetricCryptography.Utils;
using AsymmetricCryptography.Generators.LehmerGenerators;

namespace AsymmetricCryptography.LabWorks
{
    public class Lab3
    {
        private static Stopwatch stopwatch = new Stopwatch();
        static void Main()
        {
            var ticks = (uint)(int)DateTime.Now.Ticks;
            var generatorA = new LehmerHigh(ticks);

            for (int i = 0; i <= 8 * 3; i++)
            {
                var a = generatorA.RandomBits(i);
                if (i < 10) System.Console.WriteLine($"{i}  | {Tools.ToString(a)}");
                else System.Console.WriteLine($"{i} | {Tools.ToString(a)}");
            }
        }
    }
}