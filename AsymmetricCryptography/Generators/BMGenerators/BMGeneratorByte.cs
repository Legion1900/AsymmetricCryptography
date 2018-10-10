using System;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Globalization;

namespace symmetricCryptography.Generators.BMGenerators
{
    public class BMGeneratorByte: BMGenerator
    {
        public static void Result(){
            BigInteger p = RandomIntegerBetween(0, P);
			Console.Write("base: " + A + "\nexponent: " + p + "\nmodulus: " + P);

            GenerateSequence(p, 100);
            int count = 0;
            Console.Write("\n\n");
            foreach(byte b in GenerateSequence(p, 100)){
                Console.Write(b.ToString("X1"));
                count++;
                Console.Write(" ");
                if(count % 32 == 1 && count != 1) Console.Write("\n");
            }
            Console.Write("\n");
        }

        public static byte[] GenerateSequence(BigInteger seed, int size){
            Byte[] bytes = new Byte[size];
            bytes[0] = (byte)(int)((seed * 256) / (P - 1));
			T = seed;

            for(int i = 1; i < size; i++){
                bytes[i] = (byte)(int)((T * 256) / (P - 1));
            }

            return bytes;// bytes;
        }
    }
}