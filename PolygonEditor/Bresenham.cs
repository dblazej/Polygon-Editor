using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonEditor
{
    class Bresenham
    {
        public static void PutPixel(Bitmap bm, int x, int y, Color color)
        {
            if (x < 0 || y < 0 || x > bm.Width - 5 || y > bm.Height - 5) return;
            bm.SetPixel(x, y, color);
        }
        public static void DrawLine(Bitmap bm, PaintEventArgs e, Point p1, Point p2, bool bresenham, Color color)
        {
            if (bresenham)
            {
                int w = (int)(p2.X - p1.X);
                int h = (int)(p2.Y - p1.Y);
                int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;

                if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
                if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
                if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;

                int longest = Math.Abs(w);
                int shortest = Math.Abs(h);
                if (!(longest > shortest))
                {
                    longest = Math.Abs(h);
                    shortest = Math.Abs(w);
                    if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                    dx2 = 0;
                }

                int numerator = longest >> 1;
                int x = (int)p1.X;
                int y = (int)p1.Y;
                for (int i = 0; i <= longest; i++)
                {
                    PutPixel(bm, x, y, color);
                    numerator += shortest;
                    if (!(numerator < longest))
                    {
                        numerator -= longest;
                        x += dx1;
                        y += dy1;
                    }
                    else
                    {
                        x += dx2;
                        y += dy2;
                    }
                }
            }
            else e.Graphics.DrawLine(new Pen(color), p1, p2);
        }
    }
}