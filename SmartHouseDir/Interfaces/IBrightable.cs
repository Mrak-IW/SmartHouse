using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface IBrightable
	{
		IAdjustable<int> Regulator { get; set; }
		int Brightness { get; set; }
		int BrightnessMax { get; set; }
		int BrightnessMin { get; set; }
		int BrightnessStep { get; set; }

		void IncreaseBrightness();
		void DecreaseBrightness();
	}
}