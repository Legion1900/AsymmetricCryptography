using System;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Diagnostics;
using NeinMath;

namespace Generators.src.BMGenerators
{
    public class BMGeneratorBit: BMGenerator
    {

        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/BMGeneratorBitOut.txt";

        public static void Result(int size)
        {
            double bTime, cTime;

			Integer seed = RandomIntegerBetween(0, P);
            
			BitArray bitRes = GenerateSequence(seed, size);
            // stopwatch is restarted inside of GenerateSequence(p, size) function
            bTime = (double)stopwatch.ElapsedMilliseconds / 1000;

            stopwatch.Restart();
            WriteToFile(Path, BitToString(bitRes), seed);
            stopwatch.Stop();
            cTime = (double)stopwatch.ElapsedMilliseconds / 1000;
            

            Console.WriteLine
            ("\nBlum–Micali BIT generator" + 
            "\nbase: " + A + "\nexponent (seed): " + seed + "\nmodulus: " + P +
            "\nTime elapsed for sequence generation: " + bTime + " seconds" + 
            "\nTime elapsed for converting to string and writing to file: " + cTime + " seconds");
	    }

        public static String BitToString(BitArray bitArray)
        {
            StringBuilder sb = new StringBuilder(bitArray.Length / 4);

			for (int i = 0; i < bitArray.Length; i += 4) 
            {
				int v = (bitArray[i] ? 8 : 0) | 
						(bitArray[i + 1] ? 4 : 0) | 
						(bitArray[i + 2] ? 2 : 0) | 
						(bitArray[i + 3] ? 1 : 0);
				
				sb.Append(v.ToString("X1"));
			}

			String tmp = sb.ToString();
            String result = "";

            for(int i = 0; i < tmp.Length; i++)
            {
                result += tmp[i];
                if (i == tmp.Length - 1) break;
                if(i % 2 == 1) result += " ";
            }

            return result;
        }

        public static BitArray GenerateSequence(Integer seed, int size)
        {
            stopwatch.Restart();
			// since we need to convert our bit array to bytes when we're done
            // we want the size to be devided by 8 (aka byte size in bits)
            size *= 8;
            
            BitArray bitArrayRes = new BitArray(size);
            bitArrayRes.Set(0, (seed < (P - 1) / 2 ? true : false));
            
			T = seed;

            for (int i = 1; i < size; i++)
            {
                if (T < (P - 1) / 2)
                {
                    bitArrayRes.Set(i, true);
                } else
                {
                    bitArrayRes.Set(i, false);
                }
            }           

            return bitArrayRes;
        }
    }
}