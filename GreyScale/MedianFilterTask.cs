using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static double[,] MedianFilter(double[,] original)
        {
            var width = original.GetLength(0);
            var height = original.GetLength(1);
            var medianArray = new double[width, height];
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    medianArray[x, y] = GetValueAroundPixel(x, y, original);

            return medianArray;
        }

        public static double GetValueAroundPixel(int x, int y, double[,] defaultArray)
        {
            var width = defaultArray.GetLength(0);
            var height = defaultArray.GetLength(1);
            var arrayValues = new List<double>();
            
            if (x == 0 && y == 0)
            {
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++)
                        arrayValues.Add(defaultArray[x + i, y + j]);
            }
            
            else if (x == width && y == 0)
            {
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++) 
                        arrayValues.Add(defaultArray[x - 1 + i, y + j]);
            }
            
            else if (x == 0 && y == height)
            {
                for (int i = 0; i < 2; i++)
                    for (int j = 0; j < 2; j++) 
                        arrayValues.Add(defaultArray[x + i, y - 1 + j]);
            }
            
            else if (x == width && y == height)
            {
                for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++) 
                    arrayValues.Add(defaultArray[x - 1 + i, y - 1 + j]);
            }

			
            arrayValues.Sort();
            return GetResultMedianValue(arrayValues);
        }

        public static double GetResultMedianValue(List<double> sortedArray)
        {
            return sortedArray.Count % 2 == 0 ? ((sortedArray[(sortedArray.Count / 2)] +
                                                  sortedArray[(sortedArray.Count / 2)-1]) / 2)
                : sortedArray[(sortedArray.Count / 2)];
        }
    }
}

// if (((x - 1 + i) > -1 && (y - 1 + j) > -1) && ((x - 1 + i) < width && (y - 1 + j) < height))