using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface ISmartDevice
	{
		string Name { get; }
		string DeviceType { get; }
		EPowerState State { get; set; }
		ISmartHouse Parent { get; set; }

		void On();
		void Off();
		void Break();
	}
}