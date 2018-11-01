using System;
using NeinMath;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.Cryptosystems
{
    public partial class RSAProvider
    {
        public class KeyAgreementProvider
        {
            public RSAProvider provider = new RSAProvider();

            public (Integer k1, Integer s1) SendKey(Integer k)
            {
                if (provider.ExternalPublicKey.Equals((0, 0)))
                {
                    throw  new InvalidOperationException("ExternalPublicKey has not been set!");
                }
                if (k < 2 || k > provider.InternalPublicKey.n)
                {
                    throw new ArgumentOutOfRangeException("k is higher then 1 or lower then InternalPublicKey.n!");
                }

                var k1 = k.ModPow(provider.ExternalPublicKey.e, provider.ExternalPublicKey.n);
                var s = k.ModPow(provider.d, provider.InternalPublicKey.n);
                var s1 = s.ModPow(provider.ExternalPublicKey.e, provider.ExternalPublicKey.n);

                return (k1, s1);
            }

            public bool ReceiveKey((Integer k1, Integer s1) key)
            {
                // *! in ReceiveKey method we are working as an user B
                // *! so Internal.d and Internal.n values are secret and public keys of the user B
                // *! this way, External.e and External.n are public key of an user A
                var k = key.k1.ModPow(provider.d, provider.InternalPublicKey.n);
                var s = key.s1.ModPow(provider.d, provider.InternalPublicKey.n);

                System.Console.WriteLine($"received k = {k}");
                return (k == s.ModPow(provider.ExternalPublicKey.e, provider.ExternalPublicKey.n));
            }
        }
    }
}