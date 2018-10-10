namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public class L89 : Register
    {
        private const int Length = 89;

        public override char NextBit()
        {
            char newBit = (register[51] ^ register[0]).ToString().ToCharArray()[0];
            register.Add(newBit);
            char output = register[0];
            register.RemoveAt(0);
            return output;
        }
        
        public L89(long seed) : base(seed, Length)
        {}
    }
}