using System;
using System.Numerics;
using System.Diagnostics;
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
            var users = RSAProvider.KeyAgreementProvider.GetUsers();
            var userA = users.a;
            var userB = users.b;     

            if (userA.provider.InternalPublicKey.n > userB.provider.InternalPublicKey.n)
            {
                var tmp = userB;
                userB = userA;
                userA = tmp;
            }       

            userA.provider.ExternalPublicKey = userB.provider.InternalPublicKey;
            userB.provider.ExternalPublicKey = userA.provider.InternalPublicKey;

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
    }
}