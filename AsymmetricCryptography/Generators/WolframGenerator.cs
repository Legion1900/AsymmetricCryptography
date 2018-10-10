using System;
using System.Collections;
using Generators.src.BMGenerators;

namespace AsymmetricCryptography.Generators
{
    public class WolframGenerator
    {
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

        private static Random rnd = new Random();

        public static void Result(int size)
        {
            int seed = rnd.Next();
            BitArray bitRes = GenerateSequence(seed, size);
            
            Console.WriteLine("\nWolfram BIT generator" + 
            "\nbase (seed): " + seed + 
            "\n\n" + BMGeneratorBit.BitToString(bitRes) + "\n");
        }

        private static BitArray GenerateSequence(int seed, int size)
        {
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
    }
}