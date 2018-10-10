using System;
using System.Numerics;
using Generators.src.BMGenerators;


namespace AsymmetricCryptography.Generators.BBSGenerators
{
    public class BBSGeneratorByte: BBSGenerator
    {
        public static void Result(int size)
        {
            BigInteger p = RandomIntegerAbove(2);
            Byte[] byteResult = GenerateSequence(p, size);

            Console.WriteLine("\nBlum–Blum–Shub BYTE generator" + 
            "\nbase (seed): " + p + "\nmodulus: " + N + 
            "\n\n" + BMGeneratorByte.ConvertForOut(byteResult) + "\n");
        }

        public static Byte[] GenerateSequence(BigInteger seed, int size)
        {
            Byte[] byteArrayRes = new Byte[size];
            R = seed;

            for (int i = 0; i < size; i++)
            {
                BigInteger r = R;
                byteArrayRes[i] = (byte)(int)(r % 256);
            }
            
            return byteArrayRes;
        }
    }
}