using System.Drawing;
using System.Runtime.CompilerServices;

namespace Mazes
{
    public static class EmptyMazeTask
    {
        static void Move(Robot robot, int stepCount,Direction direction)
        {
            for(int i=0; i<stepCount; i++)
                robot.MoveTo(direction);
        }
        public static void MoveOut(Robot robot, int width, int height)
        {
            var widthWithoutWall = width - 2;
            var heightWithoutWall = height - 2;
            Move(robot,widthWithoutWall-1,Direction.Right);
            Move(robot,heightWithoutWall-1, Direction.Down);
        }
    }
}