﻿namespace Recognizer
{
	public static class GrayscaleTask
	{
		public static double[,] ToGrayscale(Pixel[,] original)
		{
			int width = original.GetLength(0);
			int height = original.GetLength(1);
			double[,] grayScale = new double[width, height];
			for (int i = 0; i < width; i++)
			{
				for (int j = 0; j < height; j++)
				{
					grayScale[i, j] = (0.299 * original[i, j].R + 0.587 * original[i, j].G + 0.114 * original[i, j].B) / 255;
				}
			}
			
			return grayScale;
		}
	}
}