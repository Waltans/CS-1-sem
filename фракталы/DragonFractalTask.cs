using System.Drawing;
using System;
 
namespace Fractals
{
	internal static class DragonFractalTask
	{
		static void DrowFractal(Pixels pixels,int iterationsCount,Random generateFromZerotoSecond,double firstangle,double secondAngle,double x,double y)
		{
			for (int i = 0; i < iterationsCount; i++)
			{
				var x1 = 0d;
				var y1 = 0d;
				if (generateFromZerotoSecond.Next(0,2) == 1)
				{
					x1 = (x * Math.Cos(firstangle) - y * Math.Sin(firstangle)) / Math.Sqrt(2);
					y1 = (x * Math.Sin(firstangle) + y * Math.Cos(firstangle)) / Math.Sqrt(2);
				}
				else
				{
					x1 = (x * Math.Cos(secondAngle) - y * Math.Sin(secondAngle)) / Math.Sqrt(2)+ 1;
					y1 = (x * Math.Sin(secondAngle) + y * Math.Cos(secondAngle)) / Math.Sqrt(2);
				}
				x = x1;
				y = y1;
				pixels.SetPixel(x1, y1);
			}
		}
		public static void DrawDragonFractal(Pixels pixels, int iterationsCount, int seed)
		{
			var x = 1.0d;
			var y = 0.0d;
			var firstangle = Math.PI / 4;
			var secondAngle = Math.PI * 3 / 4;
			var randomGenerate = new Random();
			var generateFromZerotoSecond = new Random();

			DrowFractal(pixels, iterationsCount, generateFromZerotoSecond, firstangle, secondAngle, x, y);
		}
	}
}