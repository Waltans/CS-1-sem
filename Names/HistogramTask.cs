using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var dayCount = new Double[32];
            foreach (var totalName in names)
            {
                if (totalName.Name == name && totalName.BirthDate.Day != 1)
                {
                    dayCount[totalName.BirthDate.Day]++;
                }
            }

            var totalDate = new string[dayCount.Length - 1];
            for (int i = 0; i < totalDate.Length; i++)
            {
                totalDate[i] = (i+1).ToString();
            }

            return new HistogramData(
                string.Format("Рождаемость людей с именем '{0}'", name),
                totalDate,
                dayCount.Skip(1).ToArray());
        }
    }
}