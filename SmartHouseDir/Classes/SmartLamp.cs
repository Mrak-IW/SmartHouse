using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes
{
	[Serializable]
	public class SmartLamp : SmartDevice, IBrightable
	{
		const string devType = "лампа";

		private IAdjustable<int> dimmer;

		public SmartLamp(string name)
			: base(name)
		{
			this.Dimmer = null;
		}

		public SmartLamp(string name, IAdjustable<int> dimmer)
			: base(name)
		{
			this.Dimmer = dimmer;
		}

		public IAdjustable<int> Dimmer
		{
			get
			{
				return this.dimmer;
			}
			set
			{
				dimmer = value;
				if (value != null)
				{
					BrightnessMax = value.Max;
					BrightnessMin = value.Min;
				}
			}
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
				return Dimmer.Max;
			}

			set
			{
				if (value >= 0)
				{
					Dimmer.Max = value;
				}
				else
				{
					Dimmer.Max = 0;
				}
			}
		}

		public virtual int BrightnessMin
		{
			get
			{
				return Dimmer.Min;
			}

			set
			{
				if (value >= 0)
				{
					Dimmer.Min = value;
				}
				else
				{
					Dimmer.Min = 0;
				}
			}
		}

		public virtual int Brightness
		{
			get
			{
				return State == EPowerState.On ? Dimmer.CurrentLevel : 0;
			}

			set
			{
				Dimmer.CurrentLevel = value;
			}
		}

		public int BrightnessStep
		{
			get
			{
				return Dimmer.Step;
			}

			set
			{
				Dimmer.Step = value;
			}
		}

		public virtual void DecreaseBrightness()
		{
			if (this.State == EPowerState.On)
			{
				Dimmer.Decrease();
			}
		}

		public virtual void IncreaseBrightness()
		{
			if (this.State == EPowerState.On)
			{
				Dimmer.Increase();
			}
		}

		public override string ToString()
		{
			string res = string.Format("{0}:\t{1} ", Name, DeviceType);
			switch (State)
			{
				case EPowerState.On:
					string progress = new string('+', 10 * Brightness / BrightnessMax);
					progress = string.Format("[{2}|{0}{1}|{3}]", progress, new string(' ', 10 - progress.Length), Dimmer.Min, Dimmer.Max);

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