using System;
using System.Numerics;
using NeinMath;

namespace AsymmetricCryptography.Utils
{
    public static class MathI
    {
        public static Integer GCD(Integer a, Integer b) 
        {
            if (b == 0) 
                return a;
            else
                return GCD(b, a % b);
        }

        // ~~~RandomInteger: ()/(min)/(min, max) - returns random NeinMath Integer~~~ //
        public static Integer RandomInteger() 
        {
            Random random = new Random();
            byte[] bytes = new byte[random.Next(2, 64)];
            Integer R;

            random.NextBytes (bytes);
            bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
            R = Integer.Parse((new BigInteger (bytes)).ToString());

            return R;
        }

        public static Integer RandomInteger(Integer min) 
        {
            Random random = new Random();
            byte[] bytes = new byte [random.Next(2, 64)];
            Integer R;

            do 
            {
                random.NextBytes (bytes);
                bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = Integer.Parse((new BigInteger (bytes)).ToString());
            } while (!(min < R));

            return R;
        }

        public static Integer RandomInteger(Integer min, Integer max) 
        {
            Random random = new Random();
            byte[] bytes = max.ToByteArray ();
            Integer R;

            do 
            {
                random.NextBytes (bytes);
                bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
                R = Integer.Parse((new BigInteger (bytes)).ToString());
            } while (!(min < R && R < max));
            return R;
        }
    }
}