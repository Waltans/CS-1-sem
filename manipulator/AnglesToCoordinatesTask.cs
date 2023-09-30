using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var xPositionElbow = Manipulator.UpperArm * Math.Cos(shoulder);
            var yPositionElbow = Manipulator.UpperArm * Math.Sin(shoulder);
            var elbowPos = new PointF((float) xPositionElbow, (float) yPositionElbow);

            var elbowAngel = shoulder + elbow - Math.PI;;
            var xPositionWrist = xPositionElbow + Manipulator.Forearm * Math.Cos(elbowAngel);
            var yPositionWrist = yPositionElbow + (Manipulator.Forearm * Math.Sin(elbowAngel));
            var wristPos = new PointF((float) xPositionWrist, (float) yPositionWrist);

            var wristAngle = elbowAngel + wrist - Math.PI;
            var xPositionPalm = xPositionWrist + Manipulator.Palm * Math.Cos(wristAngle);
            var yPositionPalm = yPositionWrist + Manipulator.Palm * Math.Sin(wristAngle);
            var palmEndPos = new PointF((float) (xPositionPalm), (float) yPositionPalm);
            
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    } 
    
    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        public static double Lenght(PointF a, PointF b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X,2) + Math.Pow(b.Y - a.Y,2));
        }

        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI / 2, Manipulator.Forearm , Manipulator.UpperArm - Manipulator.Palm)]

        
        public void TestGetJointPositions(double shoulder,double elbow, double wrist,double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");  
            Assert.AreEqual(Manipulator.UpperArm, Lenght(joints[0], new PointF(0, 0)), 1e-5, "Upper length");
            Assert.AreEqual(Manipulator.Forearm, Lenght(joints[1], joints[0]), 1e-5, "Forearm length");
            Assert.AreEqual(Manipulator.Palm, Lenght(joints[2], joints[1]), 1e-5, "Palm length");
        }
    }
}