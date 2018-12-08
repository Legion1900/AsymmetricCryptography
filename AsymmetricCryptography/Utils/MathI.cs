using System;
using System.Text;
using System.Numerics;
using System.Globalization;
using NeinMath;
using AsymmetricCryptography.Generators.LehmerGenerators;

namespace AsymmetricCryptography.Utils
{
    public static class MathI
    {

        // ~~~RandomInteger: ()/(min)/(min, max) - returns random NeinMath Integer~~~ //
        public static Integer RandomI() 
        {
            var seed = (uint)DateTime.Now.Ticks;
            var generator = new LehmerHigh(seed);

            var R = Tools.ToInteger(generator.RandomBits(64 * 8));

            return R;
        }

        public static Integer RandomI(Integer min) 
        {
            var seed = (uint)DateTime.Now.Ticks;
            var generator = new LehmerHigh(seed);
            Integer R;

            do 
            {
                R = Tools.ToInteger(generator.RandomBits(64 * 8));
            } while (!(min < R));

            return R;
        }

        public static Integer RandomI(Integer min, Integer max) 
        {
            var seed = (uint)DateTime.Now.Ticks;
            var generator = new LehmerHigh(seed);
            var bitLength = Tools.BitLength(max);
            Integer R;

            do 
            {
                R = NumberTheory.Mod(
                    Tools.ToInteger(generator.RandomBits(bitLength + 1)), max + 1);
            } while (!(min < R && R < max));
            return R;
        }
        // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ //



        // GeneratePrime(int n) - where n is number of bits in a generated prime number
        public static Integer GeneratePrime(int bitLength)
        {
            var generator = new LehmerHigh((uint)(int)DateTime.Now.Ticks);

            var rand = Tools.ToInteger(generator.RandomBits(bitLength));
            rand |= 1;
            
            var size = rand * 2 - 2;

            do
            {
                rand += 2;
            }
            while(!(PrimalityTests.MillerRabin(rand) && rand != size));

            return rand;
        }
 
        // GenerateStrongPrime(int n) - where n is number of bits in a generated prime number
        public static Integer GenerateStrongPrime(int bitLength)
        {
            Integer rand;
            int i = 1;
            do
            {
                rand = 2 * i * GeneratePrime(bitLength) + 1;
                i++;
            } while(!PrimalityTests.MillerRabin(rand));
            
            return rand;
        }

        public static Integer GenerateBlumPrime(int bitLength)
        {
            Integer rand;
            var generator = new LehmerHigh((uint)(int)DateTime.Now.Ticks);
            
            do
            {
                do
                {
                    rand = Tools.ToInteger(generator.RandomBits(bitLength)); 
                    rand |= 1;
                } while ((rand - 3) % 4 != 0);
                
                while (rand != 2 * rand - 4)
                {
                    rand += 4;
                    if (PrimalityTests.MillerRabin(rand))
                    {
                        return rand;
                    }
                }
            }
            while (true);
        }

        

        // Generate a number of primes and write to file
        private static void GeneratePrimes(int n)
        {
            int i = 1, j = 3;

            StringBuilder sb = new StringBuilder();
            sb.Append(2);
            sb.Append(' ');

            while (i < n)
            {
                if(PrimalityTests.MillerRabin(j))
                {
                    if(i == n - 1){
                        sb.Append(j);
                        System.Console.WriteLine("help");
                        i++;
                    }
                    else
                    {
                        sb.Append(j);
                        sb.Append(' ');
                        i++;
                    }
                }    
                j++;
            }

            System.IO.File.WriteAllText("./Generated/primes.txt", sb.ToString());
        }
    }
}