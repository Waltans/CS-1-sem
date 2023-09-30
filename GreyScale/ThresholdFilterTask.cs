using System;
using System.Collections.Generic;
using System.Linq;


namespace Recognizer
{
	public static class ThresholdFilterTask
	{
		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
			var weight = original.GetLength(0);
			var height = original.GetLength(1);
			int whitePixels = (int)(weight * height * whitePixelsFraction);
			double totalThreshold = FindTotalThreshold(original, whitePixelsFraction, weight, height,whitePixels);
			double[,] blackAndWhiteArray = new double[weight, height];
			for (int i = 0; i < weight;i++)
				for (int j =0; j<height;j++)
					blackAndWhiteArray[i,j] = original[i, j] >= totalThreshold
						? 1.0
						: 0.0;
				
				return blackAndWhiteArray;
		}
		
		public static double FindTotalThreshold(double[,] original, double whitePixelsFraction, int weight, int height,int whitePixels)
		{
			var valuePixels = new List<double>();
			var totalThreshold = 0.0d;
			
			for (int i = 0; i < weight;i++)
				for (int j = 0; j< height; j++)
					valuePixels.Add(original[i,j]);
			
			valuePixels.Sort();

			if (whitePixels > 0 && whitePixels <= height * weight)
				totalThreshold = valuePixels[valuePixels.Count - whitePixels];
			else totalThreshold = 20;
			
			return totalThreshold;
		}
	}
}