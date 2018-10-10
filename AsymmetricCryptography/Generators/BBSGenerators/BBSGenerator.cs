using System;
using System.Numerics;
using System.Globalization;

namespace AsymmetricCryptography.Generators.BBSGenerators
{
    public class BBSGenerator
    {
        private const String hexP = "0D5BBB96D30086EC484EBA3D7F9CAEB07";
        private const String hexQ = "0425D2B9BFDB25B9CF6C416CC6E37B59C1F";

        protected static BigInteger P = BigInteger.Parse(BBSGenerator.hexP, NumberStyles.AllowHexSpecifier);
        protected static BigInteger Q = BigInteger.Parse(BBSGenerator.hexQ, NumberStyles.AllowHexSpecifier);

        protected static BigInteger N = P * Q;


        private static BigInteger _r;
        protected static BigInteger R 
        {
            get 
            {
                _r = BigInteger.Pow(_r, 2) % N;
                return _r;
            }
            set 
            {
                _r = value; //use once for initialisation with seed
            }
        }

        public static BigInteger RandomIntegerAbove(BigInteger a) 
        {
            byte[] bytes = N.ToByteArray ();
            BigInteger R;

            Random random = new Random();

            do 
            {
                random.NextBytes (bytes);
                bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = new BigInteger (bytes);
            } while (!(a <= R));

            return R;
        }
    }
}