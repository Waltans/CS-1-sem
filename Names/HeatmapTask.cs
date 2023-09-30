using System;

namespace Names
{
    internal static class HeatmapTask
    {
        static string[] DrawStrings(string[] countDays, string[] countMonths)
        {
            for (int i = 0; i < countDays.Length; i++)
            {
                if (i < countMonths.Length)
                    countMonths[i] = (i + 1).ToString();
                
                countDays[i] = (i+2) .ToString();
            }

            return countDays;
            return countMonths;
        }
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var countDays = new string[30];
            var countMonths = new string[12];
            double[,] daysMonthsBirthday = new double[countDays.Length, countMonths.Length];

            DrawStrings(countDays, countMonths);
            
            foreach (var name in names)
            {
                if (name.BirthDate.Day != 1)
                    daysMonthsBirthday[name.BirthDate.Day - 2, name.BirthDate.Month - 1]++;
            }     
               
            return new HeatmapData(
                "Пример карты интенсивностей",
                daysMonthsBirthday, 
                countDays, 
                countMonths);
        }
    }
}