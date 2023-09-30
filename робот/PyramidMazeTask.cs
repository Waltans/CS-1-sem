namespace Mazes
{
    public static class PyramidMazeTask
    {
        public static void Move(Robot robot, int steps, Direction direction)
        {
            for(; steps > 0; steps--)
            {
                robot.MoveTo(direction);
            }
        }
        public static void MoveOut(Robot robot, int width, int height)
        {
            int floorWidth = width - 3;
            while(!robot.Finished)
            {
                Move(robot, floorWidth, Direction.Right);
                Move(robot, 2, Direction.Up);
                floorWidth -= 2;
                Move(robot, floorWidth, Direction.Left);
                if(robot.Finished) break;
                Move(robot, 2, Direction.Up);
                floorWidth -= 2;
            }
        }
    }
}