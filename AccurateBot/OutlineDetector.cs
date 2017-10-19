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
	public class OutlineDetector
	{
		static Image<Bgr, byte> frame;
		int Diff(int y, int x, int y2, int x2)
		{
			return Math.Abs(frame.Data[y, x, 0] - frame.Data[y2, x2, 0]);
		}

		public void SetCursor(int x, int y)
		{

		}

		public Bitmap Update(ref Image<Bgr, byte> frame, List<int> data)
		{
			OutlineDetector.frame = frame;

			var outlines = frame.Copy();
			var gray = frame.Convert<Gray, byte>();

			for (int x = 3; x < outlines.Width - 3; x++)
			{
				for (int y = 10; y < outlines.Height - 10; y++)
				{
					if(x == 25 && y == 25)
						Console.WriteLine(Diff(y, x, y - 1, x));
					if (Diff(y, x, y - 1, x) < data[0] || 
						Diff(y, x, y - 1, x) > data[1] ||
						Diff(y, x, y + 1, x) < data[2] ||
						Diff(y, x, y + 1, x) > data[3] ||
						Diff(y, x - 1, y, x) > data[4] ||
						Diff(y, x + 1, y, x) > data[4] ||
						outlines.Data[y, x, 2] < data[5])
					{
						outlines.Data[y, x, 0] = 0;
						outlines.Data[y, x, 1] = 0;
						outlines.Data[y, x, 2] = 0;
					}
				}
			}
			var result = frame + outlines.Dilate(4);
			var bitmap = outlines.ToBitmap();
			outlines.Dispose();
			return result.ToBitmap();
		}
	}
}
