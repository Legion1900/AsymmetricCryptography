using System;
using System.Collections;
using System.Globalization;
using System.Numerics;
using System.Text;
using NeinMath;

namespace AsymmetricCryptography.Utils
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

        public static string ToString(Byte[] bytes)
        {
            String hexResult = BitConverter.ToString(bytes).Replace("-", " ");

            return hexResult;
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

        public static BitArray ToBitArray(byte[] bytes)
        {

            return null;
        }

        public static string[] GenerateByteAlphabet()
        {
            var bytes = new byte[256];
            for (Byte b = 0; ; b++)
            {
                bytes[b] = b;

                if (b == 255) break;
            }

            var bytesToString = BitConverter.ToString(bytes);
            return bytesToString.Split('-');
        }

        public static byte[] ToByteArray(BitArray n) {

            int size = (int)Math.Ceiling((double) n.Count / 8);

            byte[] bytes = new byte[size];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < n.Count; i++) 
            {
                if (n[i]) 
                {
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));
                }
                bitIndex++;
                if (bitIndex == 8) 
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }

        public static void WriteToFile(string path, string contents, Integer seed)
        {
            System.IO.File.WriteAllText(path, (contents + "\nseed:" + seed.ToString()));
        }

        public static Integer ToInteger(string hex)
        {
            return Integer.Parse(BigInteger
                .Parse(
                    hex
                        .Replace(" ", String.Empty)
                        .Insert(0, "0"),
                    NumberStyles.AllowHexSpecifier)
                .ToString());
        }
    
        public static Integer ToInteger(BitArray bits)
        {
            return Tools.ToInteger(Tools.ToString(Tools.ToByteArray(bits)));
        }

        public static int BitLength(Integer num)
        {
            return (int)num.Log(2) + 1;
        }
    }
}