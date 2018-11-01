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
        public static Integer GCD(Integer a, Integer b) 
        {
            if (b == 0) 
                return a;
            else
                return GCD(b, a % b);
        }

        // ~~~RandomInteger: ()/(min)/(min, max) - returns random NeinMath Integer~~~ //
        public static Integer RandomI() 
        {
            Random random = new Random();
            byte[] bytes = new byte[random.Next(2, 64)];
            Integer R;

            random.NextBytes (bytes);
            bytes [bytes.Length - 1] &= (byte)0x7F; //force sign bit to positive
            R = Integer.Parse((new BigInteger (bytes)).ToString());

            return R;
        }

        public static Integer RandomI(Integer min) 
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

        public static Integer RandomI(Integer min, Integer max) 
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

        
        // GeneratePrime(int n) - where n is number of bits in a generated prime number
        public static Integer GeneratePrime(int n)
        {
            String container;
            Integer random;
            var lehmerGenerator = new LehmerHigh((uint)(int)DateTime.Now.Ticks);

            do
            {
                container = Tools.ToString(
                    lehmerGenerator.RandomBytes(n)).Replace(" ", String.Empty).Insert(0, "0");

                random = Integer.Parse(BigInteger.Parse(container, NumberStyles.AllowHexSpecifier).ToString());
            }
            while(!PrimalityTests.MillerRabin(random));

            return random;
        }

        public static void GeneratePrimes(int n)
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

        // GenerateStrongPrime(int n) - where n is number of bits in a generated prime number
        public static Integer GenerateStrongPrime(int n){
            Integer prime;
            int i = 1;
            do
            {
                prime = 2 * i * GeneratePrime(n) + 1;
                i++;
            } while(!PrimalityTests.MillerRabin(prime));
            
            return prime;
        }
    }
    
}