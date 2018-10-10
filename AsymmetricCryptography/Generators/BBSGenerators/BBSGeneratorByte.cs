using System;
using System.Numerics;
using AsymmetricCryptography.Generators.BMGenerators;


namespace AsymmetricCryptography.Generators.BBSGenerators
{
    public class BBSGeneratorByte: BBSGenerator
    {
        public static void Result(){
            int size = 100; //GENERATED SEQUENCE SIZE

            BigInteger p = RandomIntegerAbove(2);
            Byte[] byteResult = GenerateSequence(p, size);

            Console.WriteLine("\nBlum–Blum–Shub BYTE generator" + 
            "\nbase (seed): " + p + "\nmodulus: " + N + 
            "\n\n" + BMGeneratorByte.ConvertForOut(byteResult) + "\n");
        }

        public static Byte[] GenerateSequence(BigInteger seed, int size){
            R = seed;
            Byte[] resBytes = new Byte[size];

            for (int i = 0; i < size; i++){
                BigInteger r = R;
                resBytes[i] = (byte)(int)(r % 256);
            }
            
            return resBytes;
        }
    }
}