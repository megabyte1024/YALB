using BenchmarkDotNet.Running;
using System;
using System.Threading;

namespace YALB
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run(typeof(Benchmark));
            //Profile();
        }

        private static void Profile()
        {
            var ben = new Benchmark();
            ben.Setup();
            for (int i = 0; i < 100000; i++)
            {
                ben.FastHydrationLinq2();
            }
        }
    }
}
