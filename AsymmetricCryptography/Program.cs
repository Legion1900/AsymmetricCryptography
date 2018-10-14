using System;
using System.IO;
using AsymmetricCryptography.Generators;
using AsymmetricCryptography.Generators.BMGenerators;
using AsymmetricCryptography.Generators.Criteria;
using AsymmetricCryptography.Generators.LehmerGenerators;
using AsymmetricCryptography.Generators.LFSRGenerators;
using Generators.src.BBSGenerators;
using Generators.src.BMGenerators;

namespace AsymmetricCryptography
{
    public static class Program
    {
        private const int N = 125000;
        private static double[] Alpha = 
            {0.01, 0.05, 0.1};
        private static double[,] TheoryChiSquared = 
        {{307.536379819, 292.145942934, 283.941473792}, 
            {65077.53638, 65062.1459429, 65053.9414738}, 
            {2347.53637982, 2332.14594293, 2323.94147379}};
            
        public static void Main()
        {
            
            Console.WriteLine("Output length (in bytes): {0}", N);
            int i = 0;
            foreach (var a in Alpha)
            {
                Console.WriteLine("Alpha: {0}",a);
                for (int j = 0; j < 3; j++)
                {
                    Console.Write("{0} ",TheoryChiSquared[i, j]);
                }

                i++;
                Console.WriteLine();
            }
            
            var seed = 636746134997083034;
            var librarianSeed = File.ReadAllText("C:\\Users\\Tyler\\Desktop\\seed.txt");

            IGenerator[] generators =
            {
                new LehmerLow(seed),
                new LehmerHigh(seed),
                new L20(seed),
                new L89(seed),
                new GeffeGenerator(seed),
                new Librarian(librarianSeed),
                new BMGeneratorBit(null),
                new BMGeneratorByte(null),
                new BBSGeneratorBit(null),
                new BBSGeneratorByte(null), 
                new WolframGenerator(null), 
            };

            foreach (var gen in generators)
            {
                RunTests(gen, N);
            }
            
            Random random = new Random();
            byte[] bytes = new byte[N];
            random.NextBytes(bytes);
            bytes[bytes.Length - 1] &= (byte) 0x7F;
            Console.WriteLine("Name: C# Random");
            Console.WriteLine("EquiprobabilityCriterion: {0}", EquiprobabilityCriterion.Test(Tools.ToString(bytes)));
            Console.WriteLine("IndependenceCriterion: {0}", IndependenceCriterion.ChiStatistics(bytes));
            Console.WriteLine("IntervalsCriterion: {0}", IntervalsCriterion.ChiStatistics(bytes));
            
        }

        private static void Print(IGenerator gen)
        {
            byte[] output;
            if (gen.GetType().Name == "Librarian")
                output = gen.RandomBytes(10);
            else
                output = gen.RandomBytes(100);

            string hexRes = BitConverter.ToString(output).Replace("-", " ");
                
            Console.WriteLine("Name: {0}", gen.GetType().Name);
            Console.WriteLine("Seed: {0}", gen.Seed);
            Console.WriteLine(hexRes);
            Console.WriteLine();
            Console.WriteLine("_______________________________");
        }

        private static void RunTests(IGenerator gen, int byteNum)
        {
            var output = gen.RandomBytes(byteNum);
            double[] chi = new double[3];
            int i = 0;
            
            chi[0] = EquiprobabilityCriterion.Test(Tools.ToString(output));
            chi[1] = IndependenceCriterion.ChiStatistics(output);
            chi[2] = IntervalsCriterion.ChiStatistics(output);
            
            Console.WriteLine("Name: {0}", gen.GetType().Name);
            if (gen.GetType().Name != "Librarian")
                Console.WriteLine("Seed: {0}", gen.Seed);
            Console.WriteLine("EquiprobabilityCriterion: {0}", chi[0]);
            foreach (var a in Alpha)
            {
                Console.Write("|| ");
                for (int j = 0; j < 3; j++)
                {
                    if (TheoryChiSquared[i, j] >= chi[j])
                    {
                        Console.Write("a = {0}: passed ||", a);
                    }
                    else
                    {
                        Console.Write("a = {0}: failed ||", a);
                    }
                }
                Console.WriteLine();
                
                i++;
            }
            Console.WriteLine("IndependenceCriterion: {0}", chi[1]);
            Console.WriteLine("IntervalsCriterion: {0}", chi[2]);
            Console.WriteLine("________________________________________");
        }
    }
}