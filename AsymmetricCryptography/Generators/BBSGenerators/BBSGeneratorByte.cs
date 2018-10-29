using System;
using System.Numerics;
using System.Diagnostics;
using AsymmetricCryptography.Generators.BBSGenerators;
using AsymmetricCryptography.Utils;
using NeinMath;


namespace AsymmetricCryptography.Generators.BBSGenerators
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

        public BBSGeneratorByte(Integer? seed) {
            if (seed == null)
            {
                this.seed = Tools.RandomInteger(2);
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