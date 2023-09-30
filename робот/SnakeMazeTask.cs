namespace Mazes
{
    public static class SnakeMazeTask
    {
        static void Move(Robot robot, int stepCount,Direction direction)
        {
            for(int i=0; i<stepCount; i++)
                robot.MoveTo(direction);
        }

        static void SnakeMove(int amountOfRow,Robot robot,int widthWithoutWall)
        {
            for (int i = 0; i <= amountOfRow-1; i++)
            {
                if (i % 2 == 0) Move(robot, widthWithoutWall - 1,Direction.Right);
                else Move(robot, widthWithoutWall - 1,Direction.Left);
                Move(robot, 2,Direction.Down);
            }
            Move(robot,widthWithoutWall -1,Direction.Left);
        }
        public static void MoveOut(Robot robot, int width, int height)
        {
            var widthWithoutWall = width - 2;
            var heightWithoutWall = height - 2;
            var amountOfRow = heightWithoutWall / 2;

            SnakeMove(amountOfRow, robot, widthWithoutWall);
        }
    }
}