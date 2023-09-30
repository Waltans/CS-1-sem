using System;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
		static void Move(Robot robot, int stepCount,Direction direction)
		{
			for(int i=0; i<stepCount; i++)
				robot.MoveTo(direction);
		}
		static void MoveDiagonal(int width, int height, Robot robot, int amountOfSteps, Direction direction,int countSteps, Direction secondDirection)
		{
			for (int i = 0; i < countSteps; i++)
			{
				Move(robot, amountOfSteps, direction);
				if (robot.Finished)
				{
					break;
				}
				Move(robot, 1, secondDirection);
			}
		}
		public static void MoveOut(Robot robot, int width, int height)
		{
			if (height > width)
			{
				var amountOfSteps = (int)Math.Round((double)height / (double)width);
				var countSteps = height / amountOfSteps - 1;
				MoveDiagonal(width, height, robot, amountOfSteps,Direction.Down,countSteps,Direction.Right);

			}
			else if (height < width)
			{
				var amountOfSteps =(int) Math.Round((double)width / (double) height);
				var countSteps = height;
				MoveDiagonal(width, height, robot, amountOfSteps,Direction.Right, countSteps, Direction.Down);
			}
		}
	}
}