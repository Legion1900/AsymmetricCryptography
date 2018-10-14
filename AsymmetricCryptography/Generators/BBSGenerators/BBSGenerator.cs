using System;
using System.Numerics;
using System.Collections;
using System.Globalization;
using NeinMath;


namespace Generators.src.BBSGenerators
{
    public abstract class BBSGenerator : IGenerator
    {
        private const String hexP = "0D5BBB96D30086EC484EBA3D7F9CAEB07";
        private const String hexQ = "0425D2B9BFDB25B9CF6C416CC6E37B59C1F";

        protected static Integer P = Integer.Parse((BigInteger.Parse(BBSGenerator.hexP, NumberStyles.AllowHexSpecifier)).ToString());
        protected static Integer Q = Integer.Parse((BigInteger.Parse(BBSGenerator.hexQ, NumberStyles.AllowHexSpecifier)).ToString());

        protected static Integer N = P * Q;


        private static Integer _r;
        protected static Integer R 
        {
            get 
            {
                _r = _r.ModPow(2, N);
                return _r;
            }
            set 
            {
                _r = value; //use once for initialisation with seed
            }
        }

        public static Integer RandomIntegerAbove(Integer a) 
        {
            byte[] bytes = N.ToByteArray ();
            Integer R;

            Random random = new Random();

            do 
            {
                random.NextBytes (bytes);
                bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = Integer.Parse((new BigInteger (bytes)).ToString());
            } while (!(a <= R));

            return R;
        }

        BitArray IGenerator.RandomBits(int n)
        {
            throw new NotImplementedException();
        }

        abstract public string Seed {get;}

        public abstract byte[] RandomBytes(int size);
    }
}