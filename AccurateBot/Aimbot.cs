using Emgu.CV;
using Emgu.CV.Structure;
using OverwatchHelper;
using RawConsoleInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccurateBot
{
	public class Aimbot
	{
		Thread aimThread;
		Point crosshair;
		Point target;
		Random random;
		public Aimbot(Point crosshair)
		{
			this.crosshair = crosshair;
			aimThread = new Thread(AimThread);
			aimThread.Start();
		}

		void AimThread()
		{
			lastMousePos = MousePos();
			mouseDelta = Point.Empty;
			random = new Random();
			Stopwatch watch = new Stopwatch();
			watch.Start();
			while (true)
			{
				//Console.WriteLine(rawInput.MaybeGetInput());
				MouseDeltaLoop();
				if (target != Point.Empty)
					AimLoop(0.001f + Convert.ToSingle(watch.ElapsedMilliseconds) / 1000);
				watch.Restart();

				Thread.Sleep(sleep);
			}
		}

		Point lastMousePos;
		public static Point mouseDelta;
		int aimRange = 100;
		int sleep = 1;
		void MouseDeltaLoop()
		{
			var mousePos = Cursor.Position;
			mouseDelta.X = mousePos.X - lastMousePos.X;
			mouseDelta.Y = mousePos.Y - lastMousePos.Y;
			lastMousePos = mousePos;
		}

		void AimLoop(float deltaTime)
		{
			var difference = crosshair.Difference(target);
			var dir = target.Direction(crosshair);
			var distance = target.Distance(crosshair);

			if (distance > aimRange)
			{
				return;
			}

			if (true)
			{
				if (Math.Abs(difference.X) > 10)
					mouse_event(0x1, Convert.ToInt32((dir.X * Math.Abs(difference.X)) / 20), 0, 0, 0);
				if (Math.Abs(difference.Y) > 10)
					mouse_event(0x1, 0, Convert.ToInt32((dir.Y * Math.Abs(difference.Y)) / 20), 0, 0);
			}
		}

		public void Update(ref Image<Bgr, byte> frame, Point target, Point offset)
		{
			if (Input.KeyDown(Input.Keys.PAGE_DOWN))
			{
				aimRange -= 1;
			}

			if (Input.KeyDown(Input.Keys.PAGE_UP))
			{
				aimRange += 1;
			}

			this.target = Point.Empty;
			frame.DrawPoint(target, Color.Yellow);
			frame.DrawPoint(crosshair, Color.White);
			frame.DrawPoint(new Point(crosshair.X + offset.X, crosshair.Y + offset.Y), Color.Tomato);
			frame.Draw(new CircleF(crosshair, aimRange), new Bgr(255, 0, 255), 1);

			Point movementOffset = Point.Empty;

			if (Input.KeyDown(Input.Keys.A))
				movementOffset.X = 3;
			if (Input.KeyDown(Input.Keys.D))
				movementOffset.X = -3;

			Point movementTarget = new Point(target.X + movementOffset.X, target.Y + movementOffset.Y);

			PointF dir = movementTarget.Direction(crosshair);
			PointF accurateTarget = new PointF(movementTarget.X + dir.X * 5, movementTarget.Y + dir.Y * 5);

			frame.DrawPoint(movementTarget, Color.Orange);

			var finalTarget = new Point(Convert.ToInt32(accurateTarget.X), Convert.ToInt32(accurateTarget.Y));
			frame.DrawPoint(finalTarget, Color.Purple);

			if (Math.Abs(mouseDelta.X) > 1 || Math.Abs(mouseDelta.Y) > 1)
			{
				var c = new System.Windows.Vector(crosshair.X, crosshair.Y);
				var d = new System.Windows.Vector(mouseDelta.X, mouseDelta.Y);
				var t = new System.Windows.Vector(finalTarget.X, finalTarget.Y);
				var a1 = System.Windows.Vector.AngleBetween(c, d);
				var a2 = System.Windows.Vector.AngleBetween(c, t);
				var diff = Math.Abs(a1 - a2);
				//Console.WriteLine(diff);

				if (target != Point.Empty)
				{
					//Console.WriteLine("Speed: " + diff / 25);
					//sleep = Convert.ToInt32(diff / 50);
					sleep = 1;
					if (sleep < 2)
						sleep = 2;
					//Console.WriteLine(crosshair.Distance(target) / 5);
					this.target = finalTarget;
					var sens = Convert.ToUInt32(crosshair.Distance(target) / 2);
					//Console.WriteLine("Sens set to: " + sens);

					if (sens < 2)
						sens = 2;
					if (sens > 20)
						sens = 20;

					//SystemParametersInfo(SPI_SETMOUSESPEED, 0, sens, 0);
				}
				else
				{
					//SystemParametersInfo(SPI_SETMOUSESPEED, 0, 10, 0);
				}

				frame.Draw(new LineSegment2D(crosshair, new Point(mouseDelta.X * 100, mouseDelta.Y * 100)), new Bgr(255, 0, 255), 1);
			}
		}

		public const UInt32 SPI_SETMOUSESPEED = 0x0071;
		[DllImport("User32.dll")]
		static extern Boolean SystemParametersInfo(
			UInt32 uiAction,
			UInt32 uiParam,
			UInt32 pvParam,
			UInt32 fWinIni);

		[DllImport("user32.dll")]
		private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

		Point MousePos()
		{
			GetCursorPos(out POINT lpPoint);
			return new Point(lpPoint.X, lpPoint.Y);
		}

		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(out POINT lpPoint);

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT
		{
			public int X;
			public int Y;

			public static implicit operator Point(POINT point)
			{
				return new Point(point.X, point.Y);
			}
		}

	}
}
