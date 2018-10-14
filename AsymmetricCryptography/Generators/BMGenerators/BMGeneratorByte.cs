using System;
using System.Text;
using System.Numerics;
using System.Diagnostics;
using NeinMath;
using AsymmetricCryptography.Generators.Criteria;

namespace AsymmetricCryptography.Generators.BMGenerators
{
    public class BMGeneratorByte: BMGenerator
    {

        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/BMGeneratorByteOut.txt";

        public static String Result(int size)
        {
            double bTime, cTime;

            Integer seed = RandomIntegerBetween(0, P);

            Byte[] byteResult = GenerateSequence(seed, size);
            bTime = (double)stopwatch.ElapsedMilliseconds / 1000;

            stopwatch.Restart();
            String output = ConvertForOut(byteResult);
            
            WriteToFile(Path, output, seed);
            stopwatch.Stop();
            cTime = (double)stopwatch.ElapsedMilliseconds / 1000;

            Console.WriteLine("\nBlum–Micali BYTE generator" +
            "\nbase: " + A + "\nexponent (seed): " + seed + "\nmodulus: " + P +
            "\nTime elapsed for sequence generation: " + bTime + " seconds" + 
            "\nTime elapsed for converting to string and writing to file: " + cTime + " seconds");

            return output;
        }

        public static String ConvertForOut(Byte[] bytes)
        {
            String hexResult = BitConverter.ToString(bytes).Replace("-", string.Empty);
            StringBuilder sb = new StringBuilder();
            int count = 0;

            foreach(char ch in hexResult.ToCharArray())
            {
                sb.Append(ch);
                if (count == hexResult.Length - 1) break;
                if (count % 2 == 1) sb.Append(" ");
                count++;
            }

            return sb.ToString();
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