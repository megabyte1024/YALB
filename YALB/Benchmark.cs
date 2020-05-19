using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace YALB
{
    [MemoryDiagnoser]
    [Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
    [Config(typeof(Config))]
    public class Benchmark
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                AddColumn(StatisticColumnRelStdDev.Instance);
            }
        }

        [ParamsAllValues]
        public bool MakeGarbage = true;
        
        [Params(1, 10, 100, 1000)]
        public int N = 100;

        private string[] _lstData;

        [GlobalSetup]
        public void Setup()
        {
            MakeGarbageConditionally();
            _lstData = GetBenchDataEnum().ToArray();
        }

        public static IEnumerable<string> GetBenchDataEnum()
        {
            var rn = Environment.NewLine;
            yield return $"ФИО:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Полных лет:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Тел.:+78886543422{rn}Возраст:22";
            yield return $"ФИО:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Возраст:22";
            yield return $"Фамилия Имя Отчество:Иванов Иван Иванович{rn}Телефон:+78886543422{rn}Полных лет:22";
        }

        public string[] GetBenchData()
        {
            return _lstData;
        }

        void MakeGarbageConditionally()
        {
            if (MakeGarbage)
            {
                MockHelper.InstanceDb();
            }
        }

        public void DoTest(Action<string> action)
        {
            MakeGarbageConditionally();
            for (int i = 0; i < N; i++)
            {
                foreach (string data in GetBenchData())
                {
                    action(data);
                }
            }
        }

        [Benchmark]
        public void ManualHydrationFair()
        {
            var parser = new ManualContactHydrator();
            DoTest((data) => parser.HydrateFairWithoutLinq(data));
        }

        [Benchmark]
        public void ManualHydrationFairLinq()
        {
            var parser = new ManualContactHydrator();
            DoTest((data) => parser.HydrateFairWithLinq(data));
        }

        [Benchmark]
        public void ManualHydration()
        {
            var parser = new ManualContactHydrator();
            DoTest((data) => parser.HydrateWithoutLinq(data));
        }

        [Benchmark]
        public void ManualHydrationLinq()
        {
            var parser = new ManualContactHydrator();
            DoTest((data) => parser.HydrateWithLinq(data));
        }

        [Benchmark]
        public void FastHydration()
        {
            var parser = new FastContactHydrator();
            DoTest((data) => parser.HydrateWithoutLinq(data));
        }

        [Benchmark]
        public void FastHydrationLinq()
        {
            var parser = new FastContactHydrator();
            DoTest((data) => parser.HydrateWithLinq(data));
        }

        [Benchmark]
        public void SlowHydration()
        {
            var parser = new SlowContactHydrator();
            DoTest((data) => parser.HydrateWithoutLinq(data));
        }

        [Benchmark]
        public void SlowHydrationLinq()
        {
            var parser = new SlowContactHydrator();
            DoTest((data) => parser.HydrateWithLinq(data));
        }

        [Benchmark]
        public void FastHydrationFairLinq()
        {
            var parser = new FastContactHydrator2();
            DoTest((data) => parser.HydrateFairWithLinq(data));
        }

        [Benchmark(Baseline = true)]
        public void FastHydrationLinq2()
        {
            var parser = new FastContactHydrator2();
            DoTest((data) => parser.HydrateWithLinq2(data));
        }
    }
}
