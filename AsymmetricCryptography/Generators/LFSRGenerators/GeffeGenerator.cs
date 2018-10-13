using System;
using System.Collections;
using System.Text;

namespace AsymmetricCryptography.Generators.LFSRGenerators
{
    public class GeffeGenerator : IGenerator
    {
        private Register[] _registers;

        public string Seed { get; }

        public GeffeGenerator(long seed)
        {
            _registers = new Register[3];
            for (int i = 0; i < 3; i++)
            {
                switch (i)
                {
                    case 0:
                    {
                        _registers[i] = new L11(seed);
                        break;
                    }
                    case 1:
                    {
                        _registers[i] = new L9(seed);
                        break;
                    }
                    case 2:
                    {
                        _registers[i] = new L10(seed);
                        break;
                    }
                }
            }

            var builder = new StringBuilder("\n");
            foreach (var reg in _registers)
            {
                builder.Append($"{reg.GetType().Name}: {reg.Seed}\n");
            }

            Seed = builder.ToString();
        }

        public byte[] RandomBytes(int n)
        {
            var bytes = new byte[n];
            for (int i = 0; i < n; i++)
            {
                bytes[i] = Tools.ToByte(RandomBits(8));
            }

            Console.WriteLine("Geffe output: {0}", Convert.ToString(bytes[0], 2));

            return bytes;
        }

        public BitArray RandomBits(int n)
        {
            var bits = new BitArray(n);
            var l11 = _registers[0].RandomBits(n);
            var l9 = _registers[1].RandomBits(n);
            var l10 = _registers[2].RandomBits(n);
            for (int i = 0; i < n; i++)
            {                
                // l10 * l11 ^ (1 ^ l10) * l9 => sum1 ^ sum2
                // sum1 = l10 * l11
                // sum2 = (1 ^ l10) * l9
                bits[i] = l10[i] & l11[i] ^ (!l10[i]) & l9[i];
            }
            
            return bits;
        }

        private class L9 : Register
        {
            private const int Length = 9;
            
            public L9(long seed) : base(seed, Length)
            {
            }

            public override BitArray RandomBits(int n)
            {
                var bits = new BitArray(n);
                for (int i = 0; i < n; i++)
                {
                    var feedback = this[0] ^ this[1] ^ this[3] ^ this[4];
                    bits[i] = Push(feedback);
                }

                return bits;
            }
        }

        private class L10 : Register
        {
            private const int Length = 10;
            
            public L10(long seed) : base(seed, Length)
            {
            }

            public override BitArray RandomBits(int n)
            {
                var bits = new BitArray(n);
                for (int i = 0; i < n; i++)
                {
                    var feedback = this[0] ^ this[3];
                    bits[i] = Push(feedback);
                }
                
                return bits;
            }
        }

        private class L11 : Register
        {
            private const int Length = 11;
            
            public L11(long seed) : base(seed, Length)
            {
            }

            public override BitArray RandomBits(int n)
            {
                var bits = new BitArray(n);
                for (int i = 0; i < n; i++)
                {
                    var feedback = this[0] ^ this[2];
                    bits[i] = Push(feedback);
                }

                return bits;
            }
        }
    }
}