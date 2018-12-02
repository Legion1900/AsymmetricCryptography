using AsymmetricCryptography.Utils;
using System.Diagnostics;
using NeinMath;
using System;

namespace AsymmetricCryptography.LabWorks
{
    public class Lab3
    {
        static void Main()
        {
            Integer[] nums = new Integer[10];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = MathI.GeneratePrime(32);
            
            for (int i = 0; i < nums.Length; i++)
            {
                System.Console.WriteLine("({0}/{1}) = {2}",
                    nums[i], nums[i + 1], MathI.JacobiSymbol(nums[i], nums[i + 1]));
            }
        }
    }
}