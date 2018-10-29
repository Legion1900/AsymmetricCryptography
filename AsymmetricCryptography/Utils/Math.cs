using NeinMath;

namespace AsymmetricCryptography.Utils
{
    public static class Math
    {
        public static Integer GCD(Integer a, Integer b) 
        {
            if (b == 0) 
                return a;
            else
                return GCD(b, a % b);
        }
    }
}