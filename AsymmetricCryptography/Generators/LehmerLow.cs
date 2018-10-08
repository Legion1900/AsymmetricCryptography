using System;
using System.Numerics;

namespace AsymmetricCryptography.Generators
{
    public class LehmerLow
    {
        public static readonly BigInteger A = BigInteger.Pow(2, 32);
        public static readonly BigInteger M = BigInteger.Pow(2, 16) + 1;
        public static readonly int C = 119;

        public BigInteger Xn
        {
            get;
            private set;
        }

        public LehmerLow(BigInteger x0)
        {
            if (x0 <= 0)
                throw new ArgumentOutOfRangeException("BigInteger x0", "Value of x0 should bigger than 0!");
            Xn = x0;
        }
        
        public 
    }
}