using System;
using System.Numerics;
using System.Globalization;

namespace AsymmetricCryptography.Generators.BMGenerators{
    public class BMGenerator {

        // hex values of p, a and q, where p = 2*q + 1
        private const String hexP = "0CEA42B987C44FA642D80AD9F51F10457690DEF10C83D0BC1BCEE12FC3B6093E3";
        private const String hexA = "05B88C41246790891C095E2878880342E88C79974303BD0400B090FE38A688356";
        private const String hexQ = "0675215CC3E227D3216C056CFA8F8822BB486F788641E85E0DE77097E1DB049F1";

        protected static BigInteger P = BigInteger.Parse(BMGenerator.hexP, NumberStyles.AllowHexSpecifier);
		protected static BigInteger A = BigInteger.Parse(BMGenerator.hexA, NumberStyles.AllowHexSpecifier);

        private static BigInteger _t;
        protected static BigInteger T {
            get {
                _t = BigPow(A, _t, P);
                return _t;
            }
            set {
				_t = value; //use once for initialisation with seed
            }
        }
    
        
        // "_base" to a power of "exponent" by a modulus || T = Pow(A, _t) (mod P)
        // using recursive power algorythm, exponentially raising to a power of 2 to speed things up
        public static BigInteger BigPow(BigInteger _base, BigInteger exponent, BigInteger modulus){
            BigInteger count = 1;
            BigInteger res = _base;

            if (exponent == 0) return 1;
            if (exponent == 1) return _base;
            
            // exponentially raise to the power of 2 till current power is either equal
            // equal to the one that's needed, or smaller then it but bigger then power/2
            // because if we raise it to the power of 2, we'll get out of bounds of what
            // we actually needed 
            do
            {
                res = BigInteger.Pow(res, 2) % modulus;
                count *= 2;
            } while (count != exponent && count <= exponent / 2);

            // if the power is smaller then what we needed, but larger then it halfed, which
            // is the most frequent variant probably on a first go, then we recoursivelly
            // call BigPow function, passing it the number, we needed to raise to power
            // and the power that is still left (exponent - count), which is the power we 
            // needed to raise minus the power we raised
            if (count < exponent) 
            {
                res *= BigPow(_base, exponent - count, modulus);

                // and if the power was odd, we just multyply it by _base value once again
                if (exponent % 2 != 0)
                {
                    res *= _base;
                }
            } 

            return res % modulus;
        }


        // Fill a byte array of a size of a BigInteger with random bytes
        public static BigInteger RandomIntegerBetween(BigInteger a, BigInteger b) {
            byte[] bytes = b.ToByteArray ();
            BigInteger R;

            Random random = new Random();

            do 
            {
                random.NextBytes (bytes);
                bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = new BigInteger (bytes);
            } while (!(a <= R && R <= b));

            return R;
        }
    }
}