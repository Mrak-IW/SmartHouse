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
	public class Clock : SmartDevice, IHaveClock
	{
		const string devType = "часы";

		public Clock(string name) : base(name) { }

		public override string DeviceType
		{
			get
			{
				return devType;
			}
		}

		public virtual DateTime Time
		{
			get
			{
				return DateTime.Now;
			}
		}

		public override string ToString()
		{
			string result = null;
			switch (State)
			{
				case EPowerState.On:
					result = string.Format("{0}:\t{1}", Name, Time.ToLongTimeString());
					break;
				case EPowerState.Off:
					result = string.Format("{0}:\t{1} выключены", Name, DeviceType);
					break;
				case EPowerState.Broken:
					result = string.Format("{0}:\t{1} сломаны", Name, DeviceType);
					break;
			}
			return result;
		}
	}
}