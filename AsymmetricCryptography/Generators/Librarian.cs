using System;
using System.Collections;
using System.Text;

namespace AsymmetricCryptography.Generators
{
    public class Librarian : IGenerator
    {
        private byte[] _seed;
        
        public string Seed { get; }

        public int Length
        {
            get { return _seed.Length; }
        }

        public Librarian(string text)
        {
            Seed = text;
            _seed = Encoding.ASCII.GetBytes(Seed);
        }
        
        public byte[] RandomBytes(int n)
        {
            byte[] output = new byte[n];
            Array.Copy(_seed, output, n);
            return output;
        }

        BitArray IGenerator.RandomBits(int n)
        {
            throw new System.NotImplementedException();
        }
    }
}