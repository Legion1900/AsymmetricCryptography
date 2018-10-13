using System;
using System.Collections;

namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public abstract class Register : IGenerator
    {
        private BitArray register;

        public string Seed { get; }
        
        protected Register(long seed, int length)
        {
            if (seed == 0)
                throw new ArgumentOutOfRangeException(nameof(seed), "Seed must be non-zero");
            
            // Maximum value that can be represented through length-bits
            var m = (long)Math.Pow(2, length) - 1;
            seed %= m + 1;
            
            register = new BitArray(length);
            register.SetAll(false);
            var state = Convert.ToString(seed, 2);
            for (int i = state.Length - 1; i >= 0; i--)
            {
                if (state[i] == '1')
                    register[i] = true;
            }
            Seed = Tools.ToString(register);
        }

        // Bit generation logic
        public abstract BitArray RandomBits(int n);

        public byte[] RandomBytes(int n)
        {
            var bytes = new byte[n];
            for (int i = 0; i < n; i++)
            {
                bytes[i] = Tools.ToByte(RandomBits(8));
            }

            return bytes;
        }

        protected bool this[int i] => register[i];

        protected bool Push(bool bit)
        {
            bool output = register[0];
            LeftShift(1);
            register[register.Count - 1] = bit;
            return output;
        }

        private void LeftShift(int n)
        {
            for (int i = 1; i < register.Count; i++)
                register[i - 1] = register[i];
            register[register.Count - 1] = false;
        }        
    }
}