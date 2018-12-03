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

        // ax + by = g = gcd(a, b)
        public static (Integer g, Integer u, Integer v) ExtendedGCD (Integer a, Integer b)
        {
            if (a == 0)
                return (b, 0, 1);
            else
            {
                var egcd = ExtendedGCD(b % a, a);
                return (egcd.g, egcd.v - (b / a) * egcd.u, egcd.u);
            }
        }

        public static Integer Mod(Integer x, Integer n)
        {
            x %= n;
            return x < 0? x + n: x;
        }

        public static Integer[] QuickSquareRoot(Integer y, (Integer p, Integer q) n)
        {
            var mp = y.ModPow((n.p + 1) / 4, n.p);
            var mq = y.ModPow((n.q + 1) / 4, n.q);
            var N = n.p * n.q;

            var egcd = ExtendedGCD(n.p, n.q);
            var output = new Integer[4];
            output[0] = Mod((egcd.u * n.p * mq + egcd.v * n.q * mp), N);
            output[1] = Mod((egcd.u * n.p * mq - egcd.v * n.q * mp), N);
            output[2] = N - output[0];
            output[3] = N - output[2];


            return output;
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

            container = Tools.ToString(
                    generator.RandomBytes(n)).Replace(" ", String.Empty).Insert(0, "0");
            random = Tools.ToInteger(container) | 1;
            var size = random * 2 - 2;

            do
            {
                random += 2;
            }
            while(!(PrimalityTests.MillerRabin(random) && random != size));

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
            Integer prime;
            String container;
            var generator = new LehmerHigh((uint)(int)DateTime.Now.Ticks);
            
            do
            {
                do
                {
                    container = Tools.ToString(
                    generator.RandomBytes(n)).Replace(" ", String.Empty).Insert(0, "0");
                    prime = Tools.ToInteger(container);
                    prime |= 1;
                } while ((prime - 3) % 4 != 0);
                
                while (prime != 2 * prime - 4)
                {
                    prime += 4;
                    if (PrimalityTests.MillerRabin(prime))
                    {
                        return prime;
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