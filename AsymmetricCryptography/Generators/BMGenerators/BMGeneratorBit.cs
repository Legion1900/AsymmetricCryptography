using System;
using System.Collections;
using System.Diagnostics;
using System.Text;
using AsymmetricCryptography.Utils;
using NeinMath;

namespace AsymmetricCryptography.Generators.BMGenerators
{
    public class BMGeneratorBit: BMGenerator
    {

        private static Stopwatch stopwatch = new Stopwatch();
        private const String Path = "./generated/BMGeneratorBitOut.txt";

        private readonly Integer seed;
        override public string Seed
        {
            get{return seed.ToString();}
        }

        public BMGeneratorBit(Integer? seed) {
            if (seed == null)
            {
                this.seed = MathI.RandomInteger(0, P);
            } 
            else
            {
                this.seed = seed.Value;
            }
        }

        override public Byte[] RandomBytes(int size){
            if (seed == 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            
            BitArray bitRes = GenerateSequence(seed, size);

            return Tools.ToByteArray(bitRes);
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