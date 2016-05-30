using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;
using HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes
{
	[Serializable]
	public class SmartLamp : SmartDevice, IBrightable
	{
		const string devType = "лампа";

		private IAdjustable<int> dimmer;

		public SmartLamp() : base(null) { }

		public SmartLamp(string name)
			: base(name)
		{
			this.Regulator = null;
		}

		public SmartLamp(string name, IAdjustable<int> dimmer)
			: base(name)
		{
			this.Regulator = dimmer;
		}

		[ForeignKey("Dimmer")]
		public override int Id { get; set; }

		public virtual Dimmer Dimmer
		{
			get
			{
				return Regulator as Dimmer;
			}
			set
			{
				Regulator = value;
			}
		}

		[NotMapped]
		public IAdjustable<int> Regulator
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

		[NotMapped]
		public override string DeviceType
		{
			get
			{
				return devType;
			}
		}

		[NotMapped]
		public virtual int BrightnessMax
		{
			get
			{
				return Regulator.Max;
			}

			set
			{
				if (value >= 0)
				{
					Regulator.Max = value;
				}
				else
				{
					Regulator.Max = 0;
				}
			}
		}

		[NotMapped]
		public virtual int BrightnessMin
		{
			get
			{
				return Regulator.Min;
			}

			set
			{
				if (value >= 0)
				{
					Regulator.Min = value;
				}
				else
				{
					Regulator.Min = 0;
				}
			}
		}

		[NotMapped]
		public virtual int Brightness
		{
			get
			{
				return State == EPowerState.On ? Regulator.CurrentLevel : 0;
			}

			set
			{
				Regulator.CurrentLevel = value;
			}
		}

		[NotMapped]
		public int BrightnessStep
		{
			get
			{
				return Regulator.Step;
			}

			set
			{
				Regulator.Step = value;
			}
		}

		public virtual void DecreaseBrightness()
		{
			if (this.State == EPowerState.On)
			{
				Regulator.Decrease();
			}
		}

		public virtual void IncreaseBrightness()
		{
			if (this.State == EPowerState.On)
			{
				Regulator.Increase();
			}
		}

		public override string ToString()
		{
			string res = string.Format("{0}:\t{1} ", Name, DeviceType);
			switch (State)
			{
				case EPowerState.On:
					string progress = new string('+', 10 * Brightness / BrightnessMax);
					progress = string.Format("[{2}|{0}{1}|{3}]", progress, new string(' ', 10 - progress.Length), Regulator.Min, Regulator.Max);

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