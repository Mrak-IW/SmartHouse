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
	public class Fridge : SmartDevice, IHaveThermostat, IOpenCloseable, IRepareable
	{
		const string devType = "холодильник";
		private IAdjustable<int> thermostat;

		public Fridge() : base(null) { }

		public Fridge(string name)
			: base(name)
		{
			this.Thermostat = null;
		}

		public Fridge(string name, IAdjustable<int> thermostat)
			: base(name)
		{
			this.Thermostat = thermostat;
		}

		[ForeignKey("Dimmer")]
		public override int Id { get; set; }

		public virtual Dimmer Dimmer
		{
			get
			{
				return Thermostat as Dimmer;
			}
			set
			{
				Thermostat = value;
			}
		}

		[NotMapped]
		public IAdjustable<int> Thermostat
		{
			get
			{
				return this.thermostat;
			}
			set
			{
				thermostat = value;
				if (value != null)
				{
					TempMax = value.Max;
					TempMin = value.Min;
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

		public virtual bool IsOpened { get; set; }

		[NotMapped]
		public virtual int Temperature
		{
			get
			{
				return Thermostat.CurrentLevel;
			}

			set
			{
				Thermostat.CurrentLevel = value;
			}
		}

		[NotMapped]
		public virtual int TempMax
		{
			get
			{
				return Thermostat.Max;
			}

			set
			{
				if (value > 10)
				{
					Thermostat.Max = 10;
				}
				else if (value < -273)
				{
					Thermostat.Max = -273;
				}
				else
				{
					Thermostat.Max = value;
				}
			}
		}

		[NotMapped]
		public virtual int TempMin
		{
			get
			{
				return Thermostat.Min;
			}

			set
			{
				if (value > 10)
				{
					Thermostat.Min = 10;
				}
				else if (value < -273)
				{
					Thermostat.Min = -273;
				}
				else
				{
					Thermostat.Min = value;
				}
			}
		}

		[NotMapped]
		public int TempStep
		{
			get
			{
				return Thermostat.Step;
			}

			set
			{
				Thermostat.Step = value;
			}
		}

		public virtual void Close()
		{
			IsOpened = false;
		}

		public virtual void DecreaseTemperature()
		{
			if (this.State == EPowerState.On)
			{
				Thermostat.Decrease();
			}
		}

		public virtual void IncreaseTemperature()
		{
			if (this.State == EPowerState.On)
			{
				Thermostat.Increase();
			}
		}

		public virtual void Open()
		{
			IsOpened = true;
		}

		public virtual bool Repare()
		{
			bool result = false;
			if (State == EPowerState.Broken)
			{
				State = EPowerState.Off;
				result = true;
			}
			return result;
		}

		public override string ToString()
		{
			string res = string.Format("{0}:\t{1} ", Name, DeviceType);
			switch (State)
			{
				case EPowerState.On:
					string progress = new string('*', 10 * (Thermostat.Max - Temperature) / (Thermostat.Max - Thermostat.Min));
					progress = string.Format("[{2}|{0}{1}|{3}]", progress, new string(' ', 10 - progress.Length), Thermostat.Min, Thermostat.Max);

					res = string.Format("{0}жужжит {1} {2}C", res, progress, Temperature);
					break;
				case EPowerState.Off:
					res = res + "не жужжит";
					break;
				case EPowerState.Broken:
					res = res + "дымит";
					break;
			}

			res = res + (IsOpened ? " : открыт" : " : закрыт");

			return res;
		}
	}
}