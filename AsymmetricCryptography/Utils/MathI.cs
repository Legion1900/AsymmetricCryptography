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

        public static (Integer g, Integer x, Integer y) ExtendedGCD (Integer a, Integer b)
        {
            if (a == 0)
                return (b, 0, 1);
            else
            {
                var egcd = ExtendedGCD(b % a, a);
                return (egcd.g, egcd.y - (b / a) * egcd.x, egcd.x);
            }
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
            var generator = new LehmerHigh((uint)(int)DateTime.Now.Ticks);

            do
            {
                container = Tools.ToString(
                    generator.RandomBytes(n)).Replace(" ", String.Empty).Insert(0, "0");

                random = Integer.Parse(BigInteger.Parse(container, NumberStyles.AllowHexSpecifier).ToString());
                // random |= (1 << (n * 8))
                
            }
            while(!PrimalityTests.MillerRabin(random));

            return random;
        }

        
        // GenerateStrongPrime(int n) - where n is number of bits in a generated prime number
        public static Integer GenerateStrongPrime(int n)
        {
            Integer prime;
            int i = 1;
            do
            {
                prime = 2 * i * GeneratePrime(n) + 1;
                i++;
            } while(!PrimalityTests.MillerRabin(prime));
            
            return prime;
        }

        public static Integer GenerateBlumPrime(int n)
        {
            Integer prime, k = 1;
            String container;
            var generator = new LehmerHigh((uint)(int)DateTime.Now.Ticks);
            do
            {
                container = Tools.ToString(
                    generator.RandomBytes(n)).Replace(" ", String.Empty).Insert(0, "0");
                prime = Tools.HexToInteger(container);
                k++;
            } while(!PrimalityTests.MillerRabin(prime));
            
            return prime;
        }

        public static Integer GenerateBlumInteger(int n) //TODO
        {
            
            return 0;
        }


        // Generate a number of primes and write to file
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

        public static Integer JacobiSymbol(Integer a, Integer b)
        {
            // Mutual simlicity check
            if (GCD(a, b) != 1)
            {
                return 0;
            }
            
            // Transition to positive numbers
            int ans = 1;
            if (a < 0)
            {
                a = -a;
                if (b % 4 == 3)
                    ans = -ans;
            }

            // Getting rid of parity
            int t;
            while (true)
            {
                t = 0;
                while (a % 2 == 0)
                {
                    t++;
                    a /= 2;
                }

                if ((t % 2 == 1) && (b % 8 == 3 || b % 8 == 5))
                    ans = -ans;
                // Quadratic reciprocity
                if (a % 4 == b % 4)
                {
                    ans = -ans;
                }
                var tmp = a;
                a = b % tmp;
                b = tmp;

                if (a == 0)
                    break;
            }

            return ans;
        }
    }
}