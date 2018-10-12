using System.Collections;

namespace AsymmetricCryptography.Generators
{
    public interface IGenerator
    {
        // Return num of 
        byte[] RandomBytes(int n);

        BitArray RandomBits(int n);
        
        string Seed { get; }
    }
}