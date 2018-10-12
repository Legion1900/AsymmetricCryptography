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
            
            var bytes = new byte[1];
            bits.CopyTo(bytes, 0);

            // Byte bits[0] has reverse order of bits
            var tmp = bytes[0];
            int n = 7;
            for (bytes[0] >>= 1; bytes[0] != 0; bytes[0] >>= 1)
            {
                tmp <<= 1;
                tmp |= (byte) (bytes[0] & 1);
                n--;
            }
            tmp <<= n;
            
            // Bits in this byte are inverted
            return tmp;
        }

        public static BitArray ToBitArray(string binary, int length = 0)
        {
            if (length == 0)
                length = binary.Length;
            BitArray output = new BitArray(length);
            output.SetAll(false);
            for (int i = binary.Length - 1; i >= 0; i--)
            {
                if (binary[i] == '1')
                    output[i] = true;
            }

            return output;
        }
    }
}