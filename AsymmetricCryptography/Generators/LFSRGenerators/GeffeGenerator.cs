//using System;
//
//namespace AsymmetricCryptography.Generators.LFSRGenerators
//{
//    public class GeffeGenerator : IGenerator
//    {
//        private int index;
//
//        // [0] - L11
//        // [1] - L9
//        // [2] - L10
//        private Register[] registers;
//
//        // seeds - could be one or more
//        public GeffeGenerator(params long[] seeds)
//        {
//            // Inappropriate usage chack
//            if (seeds == null)
//                throw new ArgumentNullException(nameof(seeds), "Seed should contain at least one nonzero element");
//            if (seeds.Length > 3)
//                throw new ArgumentOutOfRangeException(nameof(seeds), "You can pass only from 1 to 3 nonzero seeds");
//
//            // Nonzero check
//            foreach (var seed in seeds)
//            {
//                if (seed == 0)
//                    throw new ArgumentOutOfRangeException(nameof(seed), "Seeds should be nonzero");
//            }
//            
//            registers = new Register[3];
//            for (int i = 0; i < 3; i++)
//                registers[i] = null;
//
//            long tmp = 0;
//            for (int i = 0; i < 3; i++)
//            {
//                if (i < seeds.Length)
//                {
//                    tmp = seeds[i];
//                }
//                
//                switch (i)
//                {
//                    case 0:
//                    {
//                        registers[i] = new L11(tmp);
//                        break;
//                    }
//                    case 1:
//                    {
//                        registers[i] = new L9(tmp);
//                        break;
//                    }
//                    case 2:
//                    {
//                        registers[i] = new L11(tmp);
//                        break;
//                    }
//                }
//            }
//        }
//        
//        public byte NextByte()
//        {
//            throw new System.NotImplementedException();
//        }
//
//        public char NextBit()
//        {
//            // [0], [1], [2]
//            char x, y, s;
//            
//        } 
//        
//        private class L11 : Register
//        {
//            private const int Length = 11;
//            
//            public L11(long seed) : base(seed, Length)
//            {
//            }
//
//            public override char NextBit()
//            {
//                var newBit = (this[0] ^ this[2]).ToString().ToCharArray()[0];
//                return Push(newBit);
//            }
//        }
//
//        private class L9 : Register
//        {
//            private const int Length = 9;
//            
//            public L9(long seed) : base(seed, Length)
//            {
//            }
//
//            public override char NextBit()
//            {
//                var newBit = (this[0] ^ this[1] ^ this[3] ^ this[4])
//                    .ToString().ToCharArray()[0];
//                return Push(newBit);
//            }
//        }
//
//        private class L10 : Register
//        {
//            private const int Length = 10;
//            
//            public L10(long seed) : base(seed, Length)
//            {
//            }
//
//            public override char NextBit()
//            {
//                var newBit = (this[0]^ this[3]).ToString().ToCharArray()[0];
//                return Push(newBit);
//            }
//        }
//    }
//}