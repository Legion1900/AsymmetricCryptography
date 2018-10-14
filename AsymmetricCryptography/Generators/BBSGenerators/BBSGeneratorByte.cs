using System;
using System.Numerics;
using System.Diagnostics;
using AsymmetricCryptography.Generators.BMGenerators;
using NeinMath;


namespace AsymmetricCryptography.Generators.BBSGenerators
{
    public class BBSGeneratorByte: BBSGenerator
    {

        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/BBSGeneratorByteOut.txt";

        public static String Result(int size)
        {
            double bTime, cTime;

            Integer seed = RandomIntegerAbove(2);

            Byte[] byteResult = GenerateSequence(seed, size);
            bTime = (double) stopwatch.ElapsedMilliseconds / 1000;

            stopwatch.Restart();
            String output = BMGeneratorByte.ConvertForOut(byteResult);

            BMGeneratorByte.WriteToFile(Path, output, seed);
            stopwatch.Stop();
            cTime = (double) stopwatch.ElapsedMilliseconds / 1000;

            Console.WriteLine("\nBlum–Blum–Shub BYTE generator" + 
            "\nbase (seed): " + seed + "\nmodulus: " + N + 
            "\nTime elapsed for sequence generation: " + bTime + " seconds" + 
            "\nTime elapsed for converting to string and writing to file: " + cTime + " seconds");

            return output;
        }

        public static Byte[] GenerateSequence(Integer seed, int size)
        {
            stopwatch.Start();
            Byte[] byteArrayRes = new Byte[size];
            R = seed;

            for (int i = 0; i < size; i++)
            {
                byteArrayRes[i] = (byte)(int)(R % 256);
            }
            
            return byteArrayRes;
        }
    }
}