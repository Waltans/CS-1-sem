using System;

namespace DistanceTask
{
    
    public static class DistanceTask
    {
       public static double ToGetDistance(double ax, double ay, double bx, double by, double x, double y)
        {
            return Math.Sqrt((x - ax) * (x - ax) + (y - ay) * (y - ay));
        }
        public static double GetDistanceToSegment(double ax, double ay, double bx, double by, double x, double y)
        {
            if (by == ay && bx == ax)
            {
                return ToGetDistance(ax, ay, bx, by, x, y);
            }
            else if (((y - ay) * (by - ay) + (x - ax) * (bx - ax)) < 0)
            {
                return ToGetDistance(ax, ay, bx, by, x, y);
            }
            else if (((y - by) * (ay - by) + (x - bx) * (ax - bx)) < 0)
            {
                return Math.Sqrt((x - bx) * (x - bx) + (y - by) * (y - by));
            }
            return (Math.Abs((bx - ax) * (y - ay) - (by - ay) * (x - ax))) 
                   / Math.Sqrt((bx - ax) * (bx - ax) + (by - ay) * (by - ay));
        }
    }
}