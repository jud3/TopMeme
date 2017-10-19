using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AccurateBot
{
class Screenshot
	{
		[DllImport("gdi32.dll")]
		private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

		public Bitmap CaptureScreen()
		{
			throw new NotImplementedException();
		}

		public static Bitmap CaptureFov(Fov fov)
		{
			var screenCopy = new Bitmap(fov.FieldOfView.Width, fov.FieldOfView.Height, PixelFormat.Format24bppRgb);

			using (var gdest = Graphics.FromImage(screenCopy))

			using (var gsrc = Graphics.FromHwnd(IntPtr.Zero))
			{
				var hSrcDc = gsrc.GetHdc();
				var hDc = gdest.GetHdc();
				var retval = BitBlt(hDc, 0, 0, fov.FieldOfView.Width, fov.FieldOfView.Height, hSrcDc, fov.FieldOfView.X, fov.FieldOfView.Y, (int)CopyPixelOperation.SourceCopy);

				gdest.ReleaseHdc();
				gsrc.ReleaseHdc();
			}

			return screenCopy;
		}
	}
}
