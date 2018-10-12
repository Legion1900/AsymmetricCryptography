using System;
using System.Collections;
using System.Text;

namespace AsymmetricCryptography
{
    public static class Tools
    {
        public static string ToString(BitArray bits)
        {
            var builder = new StringBuilder();
            foreach (bool bit in bits)
            {
                if (bit)
                    builder.Append('1');
                else
                    builder.Append('0');
            }

            return builder.ToString();
        }

        public static byte ToByte(BitArray bits)
        {
            if (bits.Count > 8)
                throw new ArgumentOutOfRangeException(nameof(bits), "BitArray should have not more than 8 bits");
            byte num = 0;
            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    num += (byte)Math.Pow(2, i);
            }

            return num;
        }
    }
}