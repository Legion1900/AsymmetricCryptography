namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public class L20 : Register
    {
        private const int Length = 20;

        public L20(long seed) : base(seed, Length)
        {
        }

        public override char NextBit()
        {
            var newBit = (this[17] ^ this[15] ^ this[11] ^ this[0]).ToString()
                .ToCharArray()[0];
            return Push(newBit);
        }
    }
}