using System;
using System.Numerics;
using System.Collections;
using Generators.src.BMGenerators;


namespace AsymmetricCryptography.Generators.BBSGenerators
{
    public class BBSGeneratorBit: BBSGenerator
    {

        public static void Result(int size)
        {
            BigInteger p = RandomIntegerAbove(2);
            BitArray bitRes = GenerateSequence(p, size);

            Console.WriteLine("\nBlum–Blum–Shub BITE generator" + 
            "\nbase (seed): " + p + "\nmodulus: " + N + 
            "\n\n" + BMGeneratorBit.BitToString(bitRes) + "\n");
        }

        public static BitArray GenerateSequence(BigInteger seed, int size)
        {
            // since we need to convert our bit array to bytes when we're done
            // we want the size to be devided by 8 (aka byte size in bits)
            size *= 8;
            
            BitArray bitArrayRes = new BitArray(size);

            R = seed;

            for (int i = 0; i < size; i++)
            {
                bitArrayRes.Set(i, (R % 2 == 0 ? false : true));
            }

            return bitArrayRes;
        }
        
    }
}