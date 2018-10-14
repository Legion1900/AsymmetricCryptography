using System;

namespace AsymmetricCryptography.Generators.Criteria
{
    public static class IntervalsCriterion
    {
        public static double ChiSqr(byte[] bytes, int r = 10)
        {
            // m`
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
                Array.Copy(bytes, i, sequences[j], 0, length);
            }

            // Vi - how many time byte i occur in every sequence for sequence[]
            int[] byteOccurence = new int[r];
            // X ^ 2 statistic 
            double X = 0;
            for (int i = 0; i < alphabet.Length; i++)
            {
                int Vi = 0;
                
                // Initializing Vi for this byte i
                for (int j = 0; j < r; j++)
                {
                    byteOccurence[j] = ByteCount(sequences[j], alphabet[i]);
                    Vi += byteOccurence[j];
                }
                
                for (int j = 0; j < r; j++)
                {
                    // Vij - how many times byte i occur in sequence j
                    int Vij = byteOccurence[j];
                    // length = m`
                    
                    if (Vi != 0)
                        X += Math.Pow(Vij, 2) / (Vi * length);
                }
            }

            X -= 1;
            X *= bytes.Length;
            return X;
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