using System;
using System.Text;

namespace AsymmetricCryptography.Generators.Criteria
{
    public static class IntervalsCriterion
    {
        public static void ChiSqr(byte[] bytes, int r = 10)
        {
            int length = bytes.Length / r;
            byte[][] sequences = new byte[r][];
            for (int i = 0; i < sequences.Length; i++)
            {
                sequences[i] = new byte[length];
            }
            var alphabet = new byte[256];
            for (byte i = 0; ; i++)
            {
                alphabet[i] = i;
                if (i == 255)
                    break;
            }
            
//            string strBytes = Tools.ToString(bytes);
//            string[] alphabet = Tools.GenerateByteAlphabet();
//            string[] sequences = new string[bytes.Length / r];
            // Length of 1 sequence in chars = r(byte) * 2(byte = 2 symbols) + (r - 1) (spaces)
//            int length = 3 * r - 1;
//            StringBuilder builder = new StringBuilder();

//            int index = 0;
//            int tmp = 0;
//            for (int i = 0; i < strBytes.Length; i = ++tmp)
//            {
//                tmp = i + length;
//                builder.Append(strBytes);
//                sequences[index++] = builder.ToString();
//            }
            
            int endIndex = 0;
            for (int i = 0, j = 0; j < r; i = endIndex + 1, j++)
            {
                endIndex = i + length - 1;
                
                Console.WriteLine("i: {0}, i + length: {1}", i, i + length);
                
                Array.Copy(bytes, i, sequences[j], 0, length);
            }
            
            
        }

        private static int ByteCount(byte[] src, byte target)
        {
            int cnt = 0;
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i] == target)
                    cnt++;
            }

            return cnt;
        }
    }
}