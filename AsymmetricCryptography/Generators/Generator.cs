namespace AsymmetricCryptography.Generators
{
    public interface IGenerator
    {
        // Return random byte
        byte Next();
    }
}