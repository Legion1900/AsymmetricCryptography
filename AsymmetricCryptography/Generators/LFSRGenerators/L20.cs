using System;

namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public class L20 : Register
    {
        public const int Length = 20;

        public L20(long seed) : base(seed, 20)
        {
        }

        protected override char NextBit()
        {
            char newBit;
//            newBit = (char)(register[16] ^ register[14] ^ register[10] ^ register[0]);
//            newBit = (register[16] ^ register[14] ^ register[10] ^ register[0]).ToString()
//                .ToCharArray()[0];
            
            newBit = (register[17] ^ register[15] ^ register[11] ^ register[0]).ToString()
                .ToCharArray()[0];
            
//            Console.WriteLine("Input: {0} ^ {1} ^ {2} ^ {3}"
//                , register[16], register[14], register[10], register[0]);
//            Console.WriteLine("Current (int)bit: {0}", (int)newBit);
//            Console.WriteLine("Current (char)bit: {0}", newBit);
            
            register.Add(newBit);
            char output = register[0];
            register.RemoveAt(0);
            return output;
        }
    }
}