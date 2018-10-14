using System;
using System.Numerics;
using System.Collections;
using System.Diagnostics;
using AsymmetricCryptography;
using AsymmetricCryptography.Generators.BBSGenerators;
using Generators.src.BMGenerators;
using NeinMath;


namespace Generators.src.BBSGenerators
{
    public class BBSGeneratorBit: BBSGenerator
    {

        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/BBSGeneratorBitOut.txt";

        private readonly Integer seed;
        override public string Seed
        {
            get{return seed.ToString();}
        }

        // public static String Result(int size)
        // {
        //     double bTime, cTime;

        //     BitArray bitRes = GenerateSequence(seed, size);
        //     bTime = (double) stopwatch.ElapsedMilliseconds / 1000;

        //     stopwatch.Restart();
        //     String output = BMGeneratorBit.BitToString(bitRes);

        //     BMGeneratorBit.WriteToFile(Path, output, seed);
        //     stopwatch.Stop();
        //     cTime = (double) stopwatch.ElapsedMilliseconds / 1000;

        //     Console.WriteLine("\nBlum–Blum–Shub BITE generator" + 
        //     "\nbase (seed): " + seed + "\nmodulus: " + N + 
        //     "\nTime elapsed for sequence generation: " + bTime + " seconds" + 
        //     "\nTime elapsed for converting to string and writing to file: " + cTime + " seconds");

        //     return output;
        // }

        public BBSGeneratorBit(Integer? seed) {
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
            
            BitArray bitRes = GenerateSequence(seed, size);

            return Tools.ToByteArray(bitRes);
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