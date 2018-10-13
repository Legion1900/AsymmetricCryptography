using System;
using System.Numerics;
using System.Collections;
using System.Diagnostics;
using Generators.src.BMGenerators;
using NeinMath;


namespace Generators.src.BBSGenerators
{
    public class BBSGeneratorBit: BBSGenerator
    {

        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/BBSGeneratorBitOut.txt";

        public static void Result(int size)
        {
            double bTime, cTime;

            Integer seed = RandomIntegerAbove(2);

            BitArray bitRes = GenerateSequence(seed, size);
            bTime = (double) stopwatch.ElapsedMilliseconds / 1000;

            stopwatch.Restart();
            BMGeneratorBit.WriteToFile(Path, BMGeneratorBit.BitToString(bitRes), seed);
            stopwatch.Stop();
            cTime = (double) stopwatch.ElapsedMilliseconds / 1000;

            Console.WriteLine("\nBlum–Blum–Shub BITE generator" + 
            "\nbase (seed): " + seed + "\nmodulus: " + N + 
            "\nTime elapsed for sequence generation: " + bTime + " seconds" + 
            "\nTime elapsed for converting to string and writing to file: " + cTime + " seconds");

        }

        public static BitArray GenerateSequence(Integer seed, int size)
        {
            stopwatch.Start();
            // since we need to convert our bit array to bytes when we're done
            // we want the size to be devided by 8 (aka byte size in bits)
            size *= 8;
            
            BitArray bitArrayRes = new BitArray(size);

            R = seed;

            for (int i = 0; i < size; i++)
            {
                if (R % 2 != 0){
                    bitArrayRes.Set(i, true);
                }
                else
                {
                    bitArrayRes.Set(i, false);
                }
            }

            return bitArrayRes;
        }
        
    }
}