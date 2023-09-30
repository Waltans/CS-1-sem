using System;
using NUnit.Framework;

namespace Manipulation
{
    public class TriangleTask
    {
        public static double GetABAngle(double a, double b, double c)
        {
            if (a < 0 || b < 0 || c < 0) return Double.NaN;
            
            if ((0.5 * a * b) > 0) return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            
            else return Double.NaN;
        }
    }

    [TestFixture]
    public class TriangleTask_Tests
    {
        [TestCase(3, 4, 5, Math.PI / 2)]
        [TestCase(1, 1, 1, Math.PI / 3)]
        [TestCase(1, 3, 5, Double.NaN)]
        [TestCase(2, 1, 3, Math.PI)] 
        [TestCase(3, 2, 1, 0)] 
        [TestCase(0, 0, 0, Double.NaN)]

        // добавьте ещё тестовых случаев!
        public void TestGetABAngle(double a, double b, double c, double expectedAngle)
        {
            double angle = TriangleTask.GetABAngle(a, b, c);
            Assert.AreEqual(expectedAngle, angle,1e-5);
        }
    }
}