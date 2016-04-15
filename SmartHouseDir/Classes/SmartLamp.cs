using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes
{
	public class SmartLamp : SmartDevice, IBrightable
	{
		const string devType = "лампа";
		IAdjustable<int> dimmer;

		public SmartLamp(string name, IAdjustable<int> dimmer)
			: base(name)
		{
			this.dimmer = dimmer;
		}

		public override string DeviceType
		{
			get
			{
				return devType;
			}
		}

		public virtual int BrightnessMax
		{
			get
			{
				return dimmer.Max;
			}
		}

		public virtual int BrightnessMin
		{
			get
			{
				return dimmer.Min;
			}
		}

		public virtual int Brightness
		{
			get
			{
				return State == EPowerState.On ? dimmer.CurrentLevel : 0;
			}

			set
			{
				dimmer.CurrentLevel = value;
			}
		}

		public virtual void DecreaseBrightness()
		{
			if (this.State == EPowerState.On)
			{
				dimmer.Decrease();
			}
		}

		public virtual void IncreaseBrightness()
		{
			if (this.State == EPowerState.On)
			{
				dimmer.Increase();
			}
		}

		public override string ToString()
		{
			string res = string.Format("{0}:\t{1} ", Name, DeviceType);
			switch (State)
			{
				case EPowerState.On:
					string progress = new string('+', 10 * Brightness / BrightnessMax);
					progress = string.Format("[{2}|{0}{1}|{3}]", progress, new string(' ', 10 - progress.Length), dimmer.Min, dimmer.Max);

					res = string.Format("{0}включена {1} {2}лм", res, progress, Brightness);
					break;
				case EPowerState.Off:
					res = res + "выключена";
					break;
				case EPowerState.Broken:
					res = res + "неисправна";
					break;
			}

			return res;
		}
	}
}