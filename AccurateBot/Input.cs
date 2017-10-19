using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OverwatchHelper
{
	class Input
	{
		public enum Keys : ushort
		{
			LBUTTON = 0x01,
			RBUTTON = 0x02,
			XBUTTON_0 = 0x06,
			PAGE_UP = 0x21,
			PAGE_DOWN = 0x22,
			HOME = 0x24,
			CAPSLOCK = 0x14,
			LEFT = 0x25,
			UP = 0x26,
			RIGHT = 0x27,
			DOWN = 0x28,
			A = 0x41,
			D = 0x44
		}

		[DllImport("User32.dll")]
		public static extern short GetKeyState(int vKey);

		internal static bool KeyDown(Keys key)
		{
			return GetKeyState(Convert.ToInt32(key)) < 0;
		}

		internal static bool IsToggled(Keys key)
		{
			return GetKeyState(Convert.ToInt32(key) & 0x0001) != 0;
		}
	}
}
