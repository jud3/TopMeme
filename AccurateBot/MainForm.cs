using Emgu.CV;
using Emgu.CV.Structure;
using OverwatchHelper;
using RawConsoleInput;
using RawInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccurateBot
{
	public partial class MainForm : System.Windows.Forms.Form
	{
		Fov fov;
		Aimbot bot;
		Scanner scanner;
		Point boxSize = new Point(300, 300);
		Point offset = new Point(30, 30);
		OutlineDetector detector = new OutlineDetector();
		List<int> data = new List<int>();

		public MainForm()
		{
			//Process process = Process.GetProcessById(24360);
			//input = new clsGetInputID(process.Main);
			for(int i = 0; i < 100; i++)
			{
				data.Add(0);
			}

			data[0] = 15;
			data[2] = 75;
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			var rawDeviceEnumerator = new RawInputDeviceEnumerator();
			foreach (var rawInputDevice in
			   rawDeviceEnumerator.Devices.Where(
			   d => d.DeviceType == Win32.RawInputDeviceType.Mouse))
			{
				Console.WriteLine(
					"{0}:\n\tName = {1}\n\tHandle: = {2}\n",
					rawInputDevice.DeviceType,
					rawInputDevice.DeviceName,
					rawInputDevice.DeviceHandle);
					
			}

			if (File.Exists("offsetX"))
			{
				offset.X = int.Parse(File.ReadAllText("offsetX"));
				offset.Y = int.Parse(File.ReadAllText("offsetY"));
			}
			fov = new Fov { Resolution = new Point(1280, 720), FieldOfView = new Rectangle((1280 / 2) - boxSize.X / 2, (720 / 2) - boxSize.Y / 2, boxSize.X, boxSize.Y) };
			bot = new Aimbot(new Point(boxSize.X / 2, boxSize.Y / 2));
			scanner = new Scanner();
			Thread thread = new Thread(CaptureThread);
			thread.Start();
		}

		void CaptureThread()
		{
			while (true)
			{
				try
				{
					var frame = Screenshot.CaptureFov(fov);
					frameBox.Invoke((MethodInvoker)delegate
					{
						OnFrame(ref frame);
						frameBox.Image = frame;
					});

					Thread.Sleep(1);
				}
				catch

				{

				}

			}
		}

		int minSumB = 0;
		int maxSumB = 0;
		Random random = new Random();
		private int minSumG;
		private int maxSumG;
		private int minSumR;
		private int maxSumR;

		void OnFrame(ref Bitmap frame)
		{
			//Console.WriteLine(input.GetDeviceID())
			OffsetInput();

			Image<Bgr, byte> image = new Image<Bgr, byte>(frame);
			var target = scanner.FindClosestEnemy(image, new Point(boxSize.X / 2, boxSize.Y / 2), offset);

			if(Input.KeyDown(Input.Keys.CAPSLOCK))
			{
				Bitmap outlines = detector.Update(ref image, data);
				outlineBox.Image = outlines;
			}
			bot.Update(ref image, target, offset);
			frame = image.ToBitmap();
			image.Dispose();
		}

		bool letGo = true;
		void OffsetInput()
		{
			if (Input.KeyDown(Input.Keys.UP) && letGo)
			{
				letGo = false;
				offset.Y -= 2;
			}
			if (Input.KeyDown(Input.Keys.DOWN) && letGo)
			{
				letGo = false;
				offset.Y += 2;
			}
			if (Input.KeyDown(Input.Keys.LEFT) && letGo)
			{
				letGo = false;
				offset.X -= 2;
			}
			if (Input.KeyDown(Input.Keys.RIGHT) && letGo)
			{
				letGo = false;
				offset.X += 2;
			}

			if(!Input.KeyDown(Input.Keys.RIGHT) && !Input.KeyDown(Input.Keys.LEFT) && !Input.KeyDown(Input.Keys.UP) && !Input.KeyDown(Input.Keys.DOWN))
			{
				letGo = true;
			}

			if (Input.KeyDown(Input.Keys.HOME))
			{
				File.WriteAllText("offsetX", offset.X.ToString());
				File.WriteAllText("offsetY", offset.Y.ToString());
			}
		}

		private void TrackBar1_Scroll(object sender, EventArgs e)
		{
			if(listBox1.SelectedIndex != -1)
			{
				data[listBox1.SelectedIndex] = trackBar1.Value;
				textBox1.Text = data[listBox1.SelectedIndex].ToString();
			}
		}

		private void TrackBar2_Scroll(object sender, EventArgs e)
		{
		}

		private void TrackBar3_Scroll(object sender, EventArgs e)
		{
		}

		private void TrackBar4_Scroll(object sender, EventArgs e)
		{
		}

		private void TrackBar6_Scroll(object sender, EventArgs e)
		{
		}

		private void TrackBar5_Scroll(object sender, EventArgs e)
		{
		}

		private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBox1.Text = data[listBox1.SelectedIndex].ToString();
		}

		private void FrameBox_Click(object sender, EventArgs e)
		{
		}

		private void FrameBox_MouseClick(object sender, MouseEventArgs e)
		{
			detector.SetCursor(e.X, e.Y);
		}
	}
}
