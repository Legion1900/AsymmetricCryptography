using System;
using System.Diagnostics;
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

            var k = MathI.GeneratePrime(32);
            var keyA = userA.SendKey(k);
            var receiveCheck = userB.ReceiveKey(keyA);

            System.Console.WriteLine($"true k = {k} \nreceived {receiveCheck} key");
        }
    }
}