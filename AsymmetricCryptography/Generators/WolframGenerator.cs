using System;
using System.Collections;
using System.Diagnostics;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.Generators
{
    public class WolframGenerator : IGenerator
    {

        
        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/WolframGeneratorOut.txt";

        private readonly int seed;
        public String Seed
        {
            get
            { 
                return seed.ToString();
            }
        }

        private static int  _r;
        protected static int R 
        {
            get
            {
                _r = (_r << 1) ^ (_r | (_r >> 1));
                return _r;
            }
            set
            {
                _r = value;
            }
        }

        public WolframGenerator(int? seed) {
            if (seed == null)
            {
                this.seed = new Random().Next();
            } 
            else
            {
                this.seed = seed.Value;
            }
        }

        public byte[] RandomBytes(int size) {
            if (seed == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            BitArray bitRes = GenerateSequence(seed, size);

            return Tools.ToByteArray(bitRes);
        }

        private static BitArray GenerateSequence(int seed, int size)
        {
            stopwatch.Start();
            size *= 8;

            BitArray bitArrayRes = new BitArray(size);
            bitArrayRes.Set(0, (seed % 2 == 0 ? false : true));
            R = seed;

            for (int i = 1; i < size; i++)
            {
                bitArrayRes.Set(i, (R % 2 == 0 ? false : true));
            }

            return bitArrayRes;
        }

        BitArray IGenerator.RandomBits(int n)
        {
            throw new NotImplementedException();
        }
    }
}