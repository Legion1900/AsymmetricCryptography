using System;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using AsymmetricCryptography.Generators.BMGenerators;
using NeinMath;

namespace Generators.src.BMGenerators
{
    public class BMGeneratorByte: BMGenerator
    {

        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/BMGeneratorByteOut.txt";

        private readonly Integer seed;
        override public string Seed
        {
            get{return seed.ToString();}
        }

        // public static String Result(int size)
        // {
        //     double bTime, cTime;

        //     Byte[] byteResult = GenerateSequence(seed, size);
        //     bTime = (double)stopwatch.ElapsedMilliseconds / 1000;

        //     stopwatch.Restart();
        //     String output = Tools.ByteArrToString(byteResult);
            
        //     WriteToFile(Path, output, seed);
        //     stopwatch.Stop();
        //     cTime = (double)stopwatch.ElapsedMilliseconds / 1000;

        //     Console.WriteLine("\nBlum–Micali BYTE generator" +
        //     "\nbase: " + A + "\nexponent (seed): " + seed + "\nmodulus: " + P +
        //     "\nTime elapsed for sequence generation: " + bTime + " seconds" + 
        //     "\nTime elapsed for converting to string and writing to file: " + cTime + " seconds");

        //     return output;
        // }

        
        public BMGeneratorByte(Integer? seed) {
            if (seed == null)
            {
                this.seed = RandomIntegerBetween(0, P);
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
            byteArrayRes[0] = (byte)(int)((seed * 256) / (P - 1));
			T = seed;

            for(int i = 1; i < size; i++)
            {
                byteArrayRes[i] = (byte)(int)((T * 256) / (P - 1));
            }

            return byteArrayRes;
        }
    }
}