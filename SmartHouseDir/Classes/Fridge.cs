using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes
{
	public class Fridge : SmartDevice, IHaveThermostat, IOpenCloseable, IRepareable
	{
		const string devType = "холодильник";
		private IAdjustable<int> dimmer;

		public Fridge(string name, IAdjustable<int> dimmer)
			: base(name)
		{
			if (dimmer.Max > 10)
			{
				throw new ArgumentOutOfRangeException(string.Format("dimmer.Max = {0}C", dimmer.Max), "Зачем нужен холодильник, который будет греть?");
			}
			if (dimmer.Min < -273)
			{
				throw new ArgumentOutOfRangeException(string.Format("dimmer.Min = {0}C", dimmer.Min), "За нарушение законов физики, программа приговорена к ексепшену");
			}
			this.dimmer = dimmer;
		}

		public override string DeviceType
		{
			get
			{
				return devType;
			}
		}

		public virtual bool IsOpened { get; set; }

		public virtual int Temperature
		{
			get
			{
				return dimmer.CurrentLevel;
			}

			set
			{
				dimmer.CurrentLevel = value;
			}
		}

		public virtual int TempMax
		{
			get
			{
				return dimmer.Max;
			}
		}

		public virtual int TempMin
		{
			get
			{
				return dimmer.Min;
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
				dimmer.Decrease();
			}
		}

		public virtual void IncreaseTemperature()
		{
			if (this.State == EPowerState.On)
			{
				dimmer.Increase();
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
					string progress = new string('*', 10 * (dimmer.Max - Temperature) / (dimmer.Max - dimmer.Min));
					progress = string.Format("[{2}|{0}{1}|{3}]", progress, new string(' ', 10 - progress.Length), dimmer.Min, dimmer.Max);

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