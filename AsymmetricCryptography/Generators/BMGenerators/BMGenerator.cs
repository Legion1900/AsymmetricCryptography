using System;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Globalization;


namespace symmetricCryptography.Generators.BMGenerators
{
    public class BMGenerator {

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
            //T = pow(A, _t) mod P;
            set {
				_t = value;
            }
        }
    
        public static BigInteger BigPow(BigInteger _base, BigInteger exponent, BigInteger modulus){
            BigInteger count = 1;
            BigInteger res = _base;

            if (exponent % 2 == 0){
				if (exponent == 0) return 1;
				
				do{
					res = BigInteger.Pow(res, 2) % modulus;
                    count *= 2;
				}
                while (count != exponent && count <= exponent / 2);

                if (count < exponent){
                    res *= BigPow(_base, exponent - count, modulus);
                }
            } else if (exponent % 2 != 0) {
                if (exponent == 1) return _base;

                do{
					res = BigInteger.Pow(res, 2) % modulus;
                    count *= 2;
				}
                while (count != exponent - 1 && count <= exponent / 2);

                if (count < exponent){
                    count++;
                    res = res * _base * BigPow(_base, exponent - count, modulus);
                }
            }

            return res % modulus;
        }

        public static BigInteger RandomIntegerBetween(BigInteger a, BigInteger b) {
            byte[] bytes = b.ToByteArray ();
            BigInteger R;

            Random random = new Random();

            do {
                random.NextBytes (bytes);
                bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = new BigInteger (bytes);
            } while (!(a <= R && R <= b));

            return R;
        }
    }
}