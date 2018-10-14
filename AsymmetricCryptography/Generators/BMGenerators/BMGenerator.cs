using System;
using System.Numerics;
using System.Globalization;
using System.Diagnostics;
using NeinMath;


namespace AsymmetricCryptography.Generators.BMGenerators
{
    public class BMGenerator 
    {

        // hex values of p, a and q, where p = 2*q + 1
        private const String hexP = "0CEA42B987C44FA642D80AD9F51F10457690DEF10C83D0BC1BCEE12FC3B6093E3";
        private const String hexA = "05B88C41246790891C095E2878880342E88C79974303BD0400B090FE38A688356";
        private const String hexQ = "0675215CC3E227D3216C056CFA8F8822BB486F788641E85E0DE77097E1DB049F1";

        protected static Integer P = Integer.Parse((BigInteger.Parse(BMGenerator.hexP, NumberStyles.AllowHexSpecifier)).ToString());
        protected static Integer A = Integer.Parse((BigInteger.Parse(BMGenerator.hexA, NumberStyles.AllowHexSpecifier)).ToString());

        private static Integer _t;
        protected static Integer T 
        {
            get 
            {
                _t = A.ModPow(_t, P);
                return _t;
            }
            set 
            {
				_t = value; //use once for initialisation with seed
            }
        }
    
        // Fill a byte array of a size of a BigInteger with random bytes
        public static Integer RandomIntegerBetween(Integer a, Integer b) 
        {
            byte[] bytes = b.ToByteArray ();
            Integer R;

            Random random = new Random();

            do 
            {
                random.NextBytes (bytes);
                bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = Integer.Parse((new BigInteger (bytes)).ToString());
            } while (!(a <= R && R <= b));

            return R;
        }

        public static void WriteToFile(string path, string contents, Integer seed){
            System.IO.File.WriteAllText (path, (contents + "\nseed:" + seed.ToString()));
        }
    }
}