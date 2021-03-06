using System;
using System.Text.RegularExpressions;
using AsymmetricCryptography.Utils;
using Math = System.Math;

namespace AsymmetricCryptography.Generators.Criteria
{
    public class EquiprobabilityCriterion
    {
        
        public static double Test(String testSubject) {
            double count = 0;

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            // generating 256 byte array, converting it to string
            
            
            String[] bytesPatternArray = Tools.GenerateByteAlphabet();

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

            int m = CountBytes(testSubject);
            double v, n = (double) m / 256;

            int intCount = 0;
            for (int i = 0; i < 256; i++)
            {
                v = CountBytesJ(testSubject, bytesPatternArray[i]);
                intCount += (int) v;

                count += (double) Math.Pow((v - n), 2) / n;

                // if(v != 0) Console.WriteLine(i + ") " + bytesPatternArray[i] + ": " + v +
                // "\nCurrent Chi squared: " + count +  
                // "\nMath.Pow(): " + Math.Pow(v - n, 2) + 
                // "\nMath.Pow((v - n), 2) / n = " + (Math.Pow((v - n), 2) / n));
            }

            return count;
        }

        private static int CountBytesJ(String testSubject, String pattern) 
        {
            return new Regex(pattern).Matches(testSubject).Count;
        }

        internal static int CountBytes(String testSubject) {
            String output = testSubject.Replace(" ", String.Empty);

            return (output.Length / 2);
        }
    }
}