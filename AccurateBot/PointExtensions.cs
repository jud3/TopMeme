using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccurateBot
{
	public static class PointExtensions
	{
		public static float Distance(this Point one, Point two)
		{
			return Convert.ToSingle(Math.Sqrt(Math.Pow((two.X - one.X), 2) + Math.Pow((two.Y - one.Y), 2)));
		}

		public static Point Difference(this Point one, Point two)
		{
			return new Point(one.X - two.X, one.Y - two.Y);
		}

		public static PointF Direction(this Point one, Point two)
		{
			var difference = one.Difference(two);
			
			var length = Math.Sqrt(difference.X * difference.X + difference.Y * difference.Y);

			return new PointF(Convert.ToSingle(difference.X / length), Convert.ToSingle(difference.Y / length));
		}

		public static void DrawPoint(this Image<Bgr, byte> frame, Point point, Color color)
		{
			frame.Draw(new Cross2DF(new PointF(point.X, point.Y), 15, 15), new Bgr(color.B, color.G, color.R), 1);
		}
	}
}
