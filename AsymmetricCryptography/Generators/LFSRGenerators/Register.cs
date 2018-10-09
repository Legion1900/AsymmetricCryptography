using System;
using System.Collections.Generic;
using System.Text;

namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public abstract class Register : IGenerator
    {   
        protected List<char> register;

        // seed - initial state
        public Register(long seed, int length)
        {
            long m = 0;
            for (int i = 0; i < length; i++)
                m += (long)Math.Pow(2, i);
            seed %= m;

            Console.WriteLine("Seed: {0} -> {1}", Convert.ToString(seed, 2), seed);
            
            var strBits = Convert.ToString(seed, 2);
            if (strBits.Length < length)
            {
                var builder = new StringBuilder(strBits);
                while (builder.Length < 20)
                    builder.Insert(0, 0);
                strBits = builder.ToString();
            }
            register = new List<char>(strBits.ToCharArray());
        }
        
        public byte Next()
        {
            var outByte = new char[8];
            for (int i = 0; i < outByte.Length; i++)
            {
                outByte[i] = NextBit();
            }
            var tmp = new string(outByte);
            
//            Console.Out.WriteLine("Byte to be converted: {0}", tmp);
            
            return Convert.ToByte(tmp, 2);
        }

        // Bit generation logic
        protected abstract char NextBit();
    }
}