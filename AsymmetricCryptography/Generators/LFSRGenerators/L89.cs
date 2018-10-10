namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public class L89 : Register
    {
        private const int Length = 89;

        public override char NextBit()
        {
            char newBit = (this[51] ^ this[0]).ToString().ToCharArray()[0];
            return Push(newBit);
        }
        
        public L89(long seed) : base(seed, Length)
        {}
    }
}