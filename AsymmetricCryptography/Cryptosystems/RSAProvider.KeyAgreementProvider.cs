using System;
using NeinMath;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.Cryptosystems
{
    public partial class RSAProvider
    {
        public class KeyAgreementProvider
        {
            public RSAProvider provider;

            private KeyAgreementProvider()
            {
                provider = new RSAProvider();
            }

            public static (KeyAgreementProvider a, KeyAgreementProvider b) GetUsers()
            {
                var a = new KeyAgreementProvider();
                var b = new KeyAgreementProvider();
                if (a.provider.InternalPublicKey.n > b.provider.InternalPublicKey.n)
                {
                    var tmp = a.provider;
                    a.provider = b.provider;
                    b.provider = tmp;
                }

                return (a, b);
            }

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

                var k1 = provider.Encrypt(k);
                var s = provider.Encrypt(k, (provider.d, provider.InternalPublicKey.n));
                var s1 = provider.Encrypt(s);

                return (k1, s1);
            }

            public bool ReceiveKey((Integer k1, Integer s1) key)
            {
                // *! in ReceiveKey method we are working as an user B
                // *! so Internal.d and Internal.n values are secret and public keys of the user B
                // *! this way, External.e and External.n are public key of an user A
                var k = provider.Encrypt(key.k1, (provider.d, provider.InternalPublicKey.n));
                var s = provider.Encrypt(key.s1, (provider.d, provider.InternalPublicKey.n));

                System.Console.WriteLine($"received k = {k}");
                return (k == s.ModPow(provider.ExternalPublicKey.e, provider.ExternalPublicKey.n));
            }
        }
    }
}