using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

 
namespace Digger
{
    public class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { };
        }
 
        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
 
        public int GetDrawingPriority()
        {
            return 0;
        }
 
        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }
 
    public class Player : ICreature
    {
        public static int xPosition = 0;
        public static int yPosition = 0;
        private static bool PlayerCanMove(int x, int y)
        {
            return Game.Map[x, y] == null || !(Game.Map[x, y] is Sack sack);
        }
 
        public CreatureCommand Act(int x, int y)
        {   
            xPosition = x;
            yPosition = y;

            switch (Game.KeyPressed)
            {
                case Keys.Down when y < Game.MapHeight - 1 && PlayerCanMove(x, y+1):
                    return new CreatureCommand { DeltaX = 0, DeltaY = 1 };
                case Keys.Down:
                    break;

                case Keys.Up when y >= 1 && PlayerCanMove(x, y - 1):
                    return new CreatureCommand { DeltaX = 0, DeltaY = -1 };
                case Keys.Up:
                    break;

                case Keys.Right when x < Game.MapWidth - 1 && PlayerCanMove(x+1, y):
                    return new CreatureCommand { DeltaX = 1, DeltaY = 0 };
                case Keys.Right:
                    break;

                case Keys.Left when x >= 1 && PlayerCanMove(x-1, y):
                    return new CreatureCommand { DeltaX = -1, DeltaY = 0 };
                case Keys.Left:
                    break;
            }
 
            return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
        }
 
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Gold gold)
                Game.Scores += 10;

            return conflictedObject is Sack sack
                   || conflictedObject is Monster monster;
        }
 
        public int GetDrawingPriority()
        {
            return int.MinValue;
        }
 
        public string GetImageFileName()
        {
            return "Digger.png";
        }
    }
 
    public class Sack : ICreature
    {
        private int _totalDistance = 0;

        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                var map = Game.Map[x, y + 1];
				if (map == null || _totalDistance > 0 
					&& (map is Monster || map is Digger.Player))
                {
                    _totalDistance++;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                }
            }

            if (_totalDistance > 1)
            {
                _totalDistance = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            
            _totalDistance = 0;
            return new CreatureCommand() { };
        }


        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
 
    public class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand { };
        }
 
        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
 
        public int GetDrawingPriority()
        {
            return 2;
        }
 
        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    public class Monster : ICreature
    {
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            var monsterPosition = new CreatureCommand { DeltaX = 0, DeltaY = 0 };

            if (PlayerPosition())
            {
                if (Player.xPosition == x)
                {
                    if (Player.yPosition < y && MonsterCanMove(x,y-1)) monsterPosition.DeltaY = -1;
                    else if (Player.yPosition > y && MonsterCanMove(x,y+1)) monsterPosition.DeltaY = 1;
                }
                else if (Player.yPosition == y)
                {
                    if (Player.xPosition < x && MonsterCanMove(x-1,y)) monsterPosition.DeltaX = -1;
                    else if (Player.xPosition > x && MonsterCanMove(x+1,y)) monsterPosition.DeltaX = 1;
                }
                else
                {
                    if (Player.xPosition < x && MonsterCanMove(x-1,y)) monsterPosition.DeltaX = -1;
                    else if (Player.xPosition > x && MonsterCanMove(x+1,y)) monsterPosition.DeltaX = 1;
                }
            }
            else
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };

            return monsterPosition;
        }

        private static bool MonsterCanMove(int x, int y)
        {
            if (x < 0 || y < 0 || Game.MapWidth <= x || Game.MapHeight <= y) return false;
            
            return !((Game.Map[x, y] is Sack) || (Game.Map[x, y] is Monster) || (Game.Map[x, y] is Terrain)) ||
                   (Game.Map[x, y] == null);
        }

        private static bool PlayerPosition()
        {
            for (var x = 0; x < Game.MapWidth; ++x)
            {
                for (var y = 0; y < Game.MapHeight; ++y)
                {
                    if (Game.Map.GetValue(x, y) is Player)
                    {
                        Player.xPosition = x;
                        Player.yPosition = y;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack sack ||
                    conflictedObject is Monster monster;
        }
    }
}