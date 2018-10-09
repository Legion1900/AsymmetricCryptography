namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public class L20 : Register
    {
        private const int Length = 20;
        
        public L20(long seed) : base(seed, Length)
        {}

        protected override char NextBit()
        {
            var newBit = (register[17] ^ register[15] ^ register[11] ^ register[0]).ToString()
                .ToCharArray()[0];
            register.Add(newBit);
            char output = register[0];
            register.RemoveAt(0);
            return output;
        }
    }
}