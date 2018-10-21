using System;
using NeinMath;

namespace AsymmetricCryptography.Encryption.PrimalityTests
{
    public class MillerRabinPrimality
    {
        public static bool Test(Integer toTest){
            Integer d = toTest - 1;

            for (int i = 0; i < (toTest - 1).ToByteArray().Length; i++)
            {
                if(d % 2 == 0)
                    d /= 2;
                else
                    break;
            }

        

            return false;
        }
        
    }
}