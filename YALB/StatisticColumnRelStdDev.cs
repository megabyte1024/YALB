using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace YALB
{
    public class StatisticColumnRelStdDev : IStatisticColumn
    {
        public static readonly StatisticColumnRelStdDev Instance = new StatisticColumnRelStdDev();

        public string Id => nameof(StatisticColumnRelStdDev) + "." + ColumnName;

        public string ColumnName => "RelStdDev";

        public bool AlwaysShow => true;

        public ColumnCategory Category => ColumnCategory.Statistics;

        public int PriorityInCategory => 0;

        public bool IsNumeric => true;

        public UnitType UnitType => UnitType.Dimensionless;

        public string Legend => "StdDev/Mean";

        public List<double> GetAllValues(Summary summary, SummaryStyle style)
            => summary.Reports
                .Where(report => report.ResultStatistics != null)
                .Select(report => calc(report.ResultStatistics))
                .Where(value => !double.IsNaN(value) && !double.IsInfinity(value))
                .ToList();

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
            => Format(summary, benchmarkCase.Config, summary[benchmarkCase].ResultStatistics, SummaryStyle.Default);

        public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
            => Format(summary, benchmarkCase.Config, summary[benchmarkCase].ResultStatistics, style);

        public bool IsAvailable(Summary summary) => true;

        public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;

        private string Format(Summary summary, ImmutableConfig config, Statistics statistics, SummaryStyle style)
        {
            string strRes = "NA";
            if (statistics != null)
            {
                double value = calc(statistics);
                if (!double.IsNaN(value))
                {
                    strRes = value.ToString("P", CultureInfo.InvariantCulture);
                }
            }
            return strRes;
        }

        private double calc(Statistics statistics)
        {
            return statistics.StandardDeviation / statistics.Mean;
        }

        public override string ToString() => ColumnName;
    }
}
