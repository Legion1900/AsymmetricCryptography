using System.Collections;

namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public class L20 : Register
    {
        private const int Length = 20;

        private readonly string seed;

        public L20(long seed) : base(seed, Length)
        {
        }

        public override BitArray RandomBits(int n)
        {
            BitArray bits = new BitArray(n);
            for (int i = 0; i < n; i++)
            {
                var feedback = this[17] ^ this[15] ^ this[11] ^ this[0];
                bits[i] = Push(feedback);
            }

            return bits;
        }
    }
}