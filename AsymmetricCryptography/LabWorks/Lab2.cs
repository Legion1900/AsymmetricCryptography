using System;
using System.Numerics;
using System.Globalization;
using NeinMath;
using AsymmetricCryptography.Utils;
using AsymmetricCryptography.Cryptosystems;

namespace AsymmetricCryptography.LabWorks
{
    public static class Lab2
    {
        public static (Integer a, Integer b) tuple;
        public static void Main()
        {
            Test();
        }

        private static void Demo()
        {
            var users = RSAProvider.KeyAgreementProvider.GetUsers();
            var userA = users.a;
            var userB = users.b;

            var k = MathI.GeneratePrime(6);
            var str = k.ToHexString();
            System.Console.WriteLine(k);
            System.Console.WriteLine(str);

            var a = "0" + ((Integer)7).ToHexString();
            var b = ((Integer)24).ToHexString();
            str += a + b;

            k = Integer.Parse(BigInteger.Parse(str.Insert(0, "0"), NumberStyles.AllowHexSpecifier).ToString());

            var keyA = userA.SendKey(k);
            System.Console.WriteLine(keyA);
            // var receiveCheck = userB.ReceiveKey(keyA);

            // System.Console.WriteLine($"true k = {k} \nreceived {receiveCheck} key");
        }

        private static void Test()
        {
            System.Console.Write("Enter n: ");
            Integer n = HexToInteger(Console.ReadLine());
            Integer e = ((Integer)2).Pow(16) + 1;

            var user = RSAProvider.KeyAgreementProvider.GetUser((e, n));
            System.Console.WriteLine($"n = {user.PublicKey.n.ToHexString()}\ne = {user.PublicKey.e.ToHexString()}");

            System.Console.Write("\nEnter k1: ");
            string k1 = Console.ReadLine();
            System.Console.Write("\nEnter s1: ");
            string s1 = Console.ReadLine();
            System.Console.WriteLine(user.ReceiveKey((HexToInteger(k1), HexToInteger(s1))));


            
        }

        private static Integer HexToInteger(string hex)
        {
            return Integer.Parse(BigInteger
                .Parse(hex.Insert(0, "0"), NumberStyles.AllowHexSpecifier)
                .ToString());
        }
    }
}