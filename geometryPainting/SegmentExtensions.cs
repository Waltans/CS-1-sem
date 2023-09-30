using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> myColor = new Dictionary<Segment, Color>();
        
        public static Color GetColor(this Segment segment)
        {
            return myColor.ContainsKey(segment) ? myColor[segment] : Color.Black;
        }
        public static void SetColor(this Segment segment, Color color)
        {
            if (myColor.ContainsKey(segment))
                myColor[segment] = color;
            else
                myColor.Add(segment, color);
        }
    }
}
