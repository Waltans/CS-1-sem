using System;
using System.Collections.Generic;
using System.Drawing;

namespace RoutePlanning
{
    public static class PathFinderTask
    {
        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
        {
            var quickestDistance = double.MaxValue;
            var countCheckpoints = checkpoints.Length;
            var quickestWay = new int[countCheckpoints];
            var way = new int[countCheckpoints];

            GetQuickestDistance(checkpoints, quickestWay, way, 1, quickestDistance);
            return quickestWay;
        }

        private static double GetQuickestDistance(Point[] checkpoints, int[] quickestWay, int[] way, int position, double quickestDistance)
        {
            var countCheckpoints = checkpoints.Length;
            var distance = checkpoints.GetPathLength(way);
            var length = way.Length;

            if (distance < quickestDistance && position == length)
            {
                return UpdateQuickestDistance(quickestDistance, distance, way, countCheckpoints, quickestWay);
            }

            for (var i = 0; i < countCheckpoints; i++)
            {
                var index = Array.IndexOf(way, i, 0, position);

                if (index != -1)
                    continue;
                way[position] = i;

                quickestDistance = GetQuickestDistance(checkpoints, quickestWay, way, position + 1, quickestDistance);
            }

            return quickestDistance;
        }

        private static double UpdateQuickestDistance(double quickestDistance, double distance, int[] way, int countCheckpoints, int[] quickestWay)
        {
            quickestDistance = distance;
            Array.Copy(way, quickestWay, countCheckpoints);
            return quickestDistance;
        }
    }
}