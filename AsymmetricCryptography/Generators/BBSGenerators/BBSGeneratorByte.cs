using System;
using System.Numerics;
using System.Diagnostics;
using AsymmetricCryptography.Generators.BBSGenerators;
using Generators.src.BMGenerators;
using NeinMath;


namespace Generators.src.BBSGenerators
{
    public class BBSGeneratorByte: BBSGenerator
    {

        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/BBSGeneratorByteOut.txt";

        private readonly Integer seed;
        override public string Seed
        {
            get{return seed.ToString();}
        }

        // public static String Result(int size)
        // {
        //     double bTime, cTime;

        //     Byte[] byteResult = GenerateSequence(seed, size);
        //     bTime = (double) stopwatch.ElapsedMilliseconds / 1000;

        //     stopwatch.Restart();
        //     String output = Tools.ByteArrToString(byteResult);

        //     BMGeneratorByte.WriteToFile(Path, output, seed);
        //     stopwatch.Stop();
        //     cTime = (double) stopwatch.ElapsedMilliseconds / 1000;

        //     Console.WriteLine("\nBlum–Blum–Shub BYTE generator" + 
        //     "\nbase (seed): " + seed + "\nmodulus: " + N + 
        //     "\nTime elapsed for sequence generation: " + bTime + " seconds" + 
        //     "\nTime elapsed for converting to string and writing to file: " + cTime + " seconds");

        //     return output;
        // }

        public BBSGeneratorByte(Integer? seed) {
            if (seed == null)
            {
                this.seed = RandomIntegerAbove(2);
            } 
            else
            {
                this.seed = seed.Value;
            }
        }
        override public Byte[] RandomBytes(int size){
            if (seed == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            Byte[] byteResult = GenerateSequence(seed, size);

            return byteResult;
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