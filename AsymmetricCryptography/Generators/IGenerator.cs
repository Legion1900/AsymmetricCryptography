namespace AsymmetricCryptography.Generators
{
    public interface IGenerator
    {
        // Return random byte
        byte NextByte();

        char NextBit();
    }
}