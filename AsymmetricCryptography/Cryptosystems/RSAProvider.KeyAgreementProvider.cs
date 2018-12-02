using System;
using NeinMath;
using AsymmetricCryptography.Utils;

namespace AsymmetricCryptography.Cryptosystems
{
    public partial class RSAProvider
    {
        public class KeyAgreementProvider
        {
            private RSAProvider provider;

            public (Integer e, Integer n) PublicKey
            {
                get
                {
                    return provider.InternalPublicKey;  
                }
            }

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
                a.provider.ExternalPublicKey = b.provider.InternalPublicKey;
                b.provider.ExternalPublicKey = a.provider.InternalPublicKey;

                return (a, b);
            }

            public static KeyAgreementProvider GetUser((Integer e, Integer n) extPublicKey)
            {
                var user = new KeyAgreementProvider();
                user.provider.ExternalPublicKey = extPublicKey;
                return user;
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
                var s = provider.Sign(k).s;
                var s1 = provider.Encrypt(s);

                return (k1, s1);
            }

            public bool ReceiveKey((Integer k1, Integer s1) key)
            {
                // *! in ReceiveKey method we are working as an user B
                // *! so Internal.d and Internal.n values are secret and public keys of the user B
                // *! this way, External.e and External.n are public key of an user A
                var k = provider.Sign(key.k1).s;
                var s = provider.Sign(key.s1).s;

                System.Console.WriteLine($"received k = {k}");
                return provider.Verify((k, s));
            }
        }
    }
}