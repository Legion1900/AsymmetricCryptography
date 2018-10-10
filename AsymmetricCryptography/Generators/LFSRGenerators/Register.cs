using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public abstract class Register : IGenerator
    {   
        protected readonly List<char> register;
        
        protected Register(long seed, int length)
        {
            long m = 0;
            for (int i = 0; i < length; i++)
                m += (long)Math.Pow(2, i);
            seed %= m;

            var strBits = Convert.ToString(seed, 2);
            if (strBits.Length < length)
            {
                var builder = new StringBuilder(strBits);
                while (builder.Length < length)
                    builder.Insert(0, '0');
                strBits = builder.ToString();
            }

//            Console.WriteLine("Seed(base 10): {0}", seed);
//            Console.WriteLine("Seed(base 2): {0}", strBits);
//            Console.WriteLine("Length: {0}", strBits.Length);
            
            register = new List<char>(strBits.ToCharArray());
        }

        // seed - initial state
        public byte NextByte()
        {
            var outByte = new char[8];
            for (int i = 0; i < outByte.Length; i++)
            {
                outByte[i] = NextBit();
            }
            var tmp = new string(outByte);
            
            return Convert.ToByte(tmp, 2);
        }

        // Bit generation logic
        public abstract char NextBit();
    }
}