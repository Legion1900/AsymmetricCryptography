using System;
using NeinMath;

namespace AsymmetricCryptography.Encryption.PrimalityTests
{
    public class MillerRabinPrimality
    {
        public static bool IsLikelyPrime(Integer p)
        {
            Integer d = p - 1, modPow, x;
            int s, k = 100; // Pow(1/4, k) = probability of p being pseudo-prime
            bool check = false;

            for (s = 0; s < (p - 1).ToByteArray().Length; s++)
            {
                if(d % 2 == 0) d /= 2;
                else break;
            }
            s++;
            
            while (k != 0)
            {
                x = Tools.RandomInteger(1, p);
                if (Tools.GCD(x, p) != 1) return false;
                
                modPow = x.ModPow(d, p);
                if (modPow == 1 || modPow == p - 1) 
                {
                    k--;
                    continue;
                }

                x = x.ModPow(d, p);
                for (int i = 0; i < s; i++)
                {
                    x = x.ModPow(2, p);
                    if (x == p - 1) 
                    {
                        check = true;
                        break;
                    }
                    else if (x == 1) return false;
                }
                
                if (!check) return false;
                k--;
            }
            
            return true;
        }
        
    }
}