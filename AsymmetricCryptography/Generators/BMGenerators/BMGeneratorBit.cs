using System;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Globalization;

namespace symmetricCryptography.Generators.BMGenerators
{
    public class BMGeneratorBit: BMGenerator
    {
        public static void Result(){	
			BigInteger p = RandomIntegerBetween(0, P);
			Console.Write("base: " + A + "\nexponent: " + p + "\nmodulus: " + P);
			
			Console.Write("\n\n" + BitToString(GenerateSequence(p, 100))); // will generate sequence of 100/8 hex			
	    }

        public static String BitToString(BitArray bitArray){
            StringBuilder sb = new StringBuilder(bitArray.Length / 4);

			for (int i = 0; i < bitArray.Length; i += 4) {
				int v = (bitArray[i] ? 8 : 0) | 
						(bitArray[i + 1] ? 4 : 0) | 
						(bitArray[i + 2] ? 2 : 0) | 
						(bitArray[i + 3] ? 1 : 0);
				
				sb.Append(v.ToString("X1"));
			}

			String tmp = sb.ToString();
            String result = "";

            for(int i = 0; i < tmp.Length; i++){
                result += tmp[i];
                if(i % 2 == 1) result += " ";
                if(i % 64 == 1 && i != 1) result += "\n";
            }

            return result += "\n";
        }

        public static BitArray GenerateSequence(BigInteger seed, int size){
			size *= 8;
            BitArray bitArrayRes = new BitArray(size);
            bitArrayRes.Set(0, seed < (P - 1) / 2 ? true: false);
            
			T = seed;

            for (int i = 1; i < size; i++){
                bitArrayRes.Set(i, T < (P - 1) / 2 ? true: false);// = T < (P - 1) / 2 ? true: false;
            }

            return bitArrayRes;
        }
    }
}