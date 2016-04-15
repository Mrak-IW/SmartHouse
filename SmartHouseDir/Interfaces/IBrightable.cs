using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface IBrightable
	{
		int Brightness { get; set; }
		int BrightnessMax { get; }
		int BrightnessMin { get; }

		void IncreaseBrightness();
		void DecreaseBrightness();
	}
}