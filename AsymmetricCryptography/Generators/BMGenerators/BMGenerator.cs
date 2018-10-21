using System;
using System.Collections;
using System.Globalization;
using System.Numerics;
using NeinMath;

namespace AsymmetricCryptography.Generators.BMGenerators
{
    public abstract class BMGenerator : IGenerator
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

        public static void WriteToFile(string path, string contents, Integer seed)
        {
            System.IO.File.WriteAllText (path, (contents + "\nseed:" + seed.ToString()));
        }

        BitArray IGenerator.RandomBits(int n)
        {
            throw new NotImplementedException();
        }

        abstract public string Seed {get;}

        public abstract byte[] RandomBytes(int size);
    }
}