using System;

namespace AsymmetricCryptography.Generators.Criteria
{
    public static class IndependenceCriterion
    {
        public static double ChiStatistics(byte[] bytes)
        {
            int length = 2;
            int pareNumber = bytes.Length / length;
            byte[][] sequences = new byte[pareNumber][];
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
            
            int endIndex = 0;
            for (int i = 0, j = 0; j < pareNumber; i = endIndex + 1, j++)
            {
                endIndex = i + length - 1;
                Array.Copy(bytes, i, sequences[j], 0, length);
            }

            double X = 0;
            for (int i = 0; i < 256; i++)
            {
                for (int j = 0; j < 256; j++)
                {
                    int Vij = PareCount(sequences, (byte)i, (byte)j);
                    int Vi = GetVi(sequences, (byte)i);
                    int aj = GetAlpha(sequences, (byte) j);

                    if (Vi != 0 && aj != 0)
                    X += Math.Pow(Vij, 2) / (Vi * aj);
                }
            }

            X -= 1;
            X *= pareNumber;
            return X;
        }

        public static int PareCount(byte[][] src, byte first, byte second)
        {
            int cnt = 0;
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i][0] == first && src[i][1] == second)
                    cnt++;
            }

            return cnt;
        }

        public static int GetVi(byte[][] src, byte b)
        {
            int cnt = 0;
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i][0] == b)
                    cnt++;
            }

            return cnt;
        }

        // aj
        public static int GetAlpha(byte[][] src, byte b)
        {
            int cnt = 0;
            for (int i = 0; i < src.Length; i++)
            {
                if (src[i][1] == b)
                    cnt++;
            }

            return cnt;
        }
    }
}