using System;
using System.Text;
using System.Numerics;
using System.Collections;
using System.Globalization;

    public class BMGenerator {

        private const String hexP = "0CEA42B987C44FA642D80AD9F51F10457690DEF10C83D0BC1BCEE12FC3B6093E3";
        private const String hexA = "05B88C41246790891C095E2878880342E88C79974303BD0400B090FE38A688356";

        private const BigInteger P = BigInteger.Parse(hexP, NumberStyles.AllowHexSpecifier);
		private BigInteger A = BigInteger.Parse(hexA, NumberStyles.AllowHexSpecifier);

        private BigInteger _t;

        protected BigInteger T {
            get {
                _t = BigPow(A, _t, P);
                return _t;
            }
            set {
				_t = value;
            }
        }

        public void Result(){	
			BigInteger p = RandomIntegerBetween(0, P);
			Console.Write("base: " + A + "\nexponent: " + p + "\nmodulus: " + P);
			
			Console.Write("\n\n" + BitToString(GenerateSequence(p, 100)));		
	    }

        public String BitToString(BitArray bitArray){
            StringBuilder sb = new StringBuilder(bitArray.Length / 4);

			for (int i = 0; i < bitArray.Length; i += 4) {
				int v = (bitArray[i] ? 8 : 0) | 
						(bitArray[i + 1] ? 4 : 0) | 
						(bitArray[i + 2] ? 2 : 0) | 
						(bitArray[i + 3] ? 1 : 0);
				
				sb.Append(v.ToString("X1"));
			}

			String tmp = sb.ToString();
            String result;

            for(int i = 0; i < tmp.Length; i++){
                result += tmp[i];
                if (i % 2 == 1) result += " ";
                if (i % 32 == 1) result += "\n";
            }

            return result;
        }
		

        public BitArray GenerateSequence(BigInteger seed, int size){
            size *= 8;

            BitArray bitArrayRes = new BitArray(size);
            bitArrayRes.Set(0, seed < (P - 1) / 2 ? true: false);
            
			T = seed;

            for (int i = 1; i < size; i++){
                bitArrayRes.Set(i, T < (P - 1) / 2 ? true: false);// = T < (P - 1) / 2 ? true: false;
            }

            return bitArrayRes;
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