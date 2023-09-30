using System;
using System.Diagnostics;
using NUnit.Framework;
using static System.Double;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        public static double[] MoveManipulatorTo(double x, double y, double angle)
        {
            var xPositionWrist = x + Manipulator.Palm * Math.Cos(Math.PI - angle);
            var yPositionWrist = y + Manipulator.Palm * Math.Sin(Math.PI - angle);
            var shoulderWristDistance = Math.Sqrt(xPositionWrist * xPositionWrist + yPositionWrist * yPositionWrist);
            var elbow = TriangleTask.GetABAngle(Manipulator.UpperArm,Manipulator.Forearm,shoulderWristDistance);
            var shoulder = TriangleTask.GetABAngle(Manipulator.UpperArm,shoulderWristDistance,Manipulator.Forearm) 
                           + Math.Atan2(yPositionWrist, xPositionWrist);
            var wrist = -angle - shoulder - elbow;
            
            return IsNaN(Math.Atan2(yPositionWrist, xPositionWrist)) ? new[] { NaN, NaN, NaN } 
                : new[] { shoulder, elbow, wrist };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public static void TestMoveManipulatorTo()
        {
            var rand = new Random();
            for (var i = 0; i < 100; i++)
            {
                var randX = rand.NextDouble();
                var randY = rand.NextDouble();
                var randAngle = rand.NextDouble() * i * Math.PI;
                var result = ManipulatorTask.MoveManipulatorTo(randX, randY, randAngle);
                
                Assert.AreNotEqual(ManipulatorTask.MoveManipulatorTo(randX, randY, randAngle),
                    new[] { double.NaN, double.NaN, double.NaN });
                
                if (IsNaN(result[0])) continue;
                var trueResult = AnglesToCoordinatesTask.GetJointPositions(result[0], result[1], result[2]);
                Assert.AreEqual(randX, trueResult[2].X, 1e-5);
                Assert.AreEqual(randY, trueResult[2].Y, 1e-5);
            }
        }
    }
}