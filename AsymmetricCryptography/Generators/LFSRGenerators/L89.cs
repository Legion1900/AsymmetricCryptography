using System;
using System.Collections;

namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public class L89 : Register
    {
        private const int Length = 89;

        public override BitArray RandomBits(int n)
        {
//            bool feedback = (this[51] ^ this[0]).ToString().ToCharArray()[0];
            BitArray bits = new BitArray(n);
            for (int i = 0; i < n; i++)
            {
                var feedback = this[51] ^ this[0];
                bits[i] = Push(feedback);
            }

            return bits;
        }
        
        public L89(long seed) : base(seed, Length)
        {}
    }
}