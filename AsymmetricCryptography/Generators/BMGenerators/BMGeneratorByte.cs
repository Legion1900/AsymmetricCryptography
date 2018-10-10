using System;
using System.Text;
using System.Numerics;

namespace AsymmetricCryptography.Generators.BMGenerators
{
    public class BMGeneratorByte: BMGenerator
    {
        public static void Result(){
            int size = 100; //GENERATED SEQUENCE SIZE

            BigInteger p = RandomIntegerBetween(0, P);
            Byte[] byteResult = GenerateSequence(p, size);

            Console.WriteLine("\nBlum–Micali BYTE generator" +
            "\nbase: " + A + "\nexponent (seed): " + p + "\nmodulus: " + P +
            "\n\n" + ConvertForOut(byteResult) + "\n");
        }

        public static String ConvertForOut(Byte[] bytes){
            String hexResult = BitConverter.ToString(bytes).Replace("-", string.Empty);
            StringBuilder sb = new StringBuilder();
            int count = 0;

            foreach(char ch in hexResult.ToCharArray()){
                sb.Append(ch);
                if (count % 2 == 1) sb.Append(" ");
                if (count % 64 == 1 && count != 1) sb.Append("\n");
                count++;
            }

            return sb.ToString();
        }

        public static Byte[] GenerateSequence(BigInteger seed, int size){
            Byte[] bytes = new Byte[size];
            bytes[0] = (byte)(int)((seed * 256) / (P - 1));
			T = seed;

            for(int i = 1; i < size; i++){
                bytes[i] = (byte)(int)((T * 256) / (P - 1));
            }

            return bytes;
        }
    }
}