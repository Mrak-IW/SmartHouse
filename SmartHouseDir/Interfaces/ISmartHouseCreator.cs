using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface ISmartHouseCreator
	{
		ISmartDevice CreateDevice(string typename, string deviceName);
		ISmartHouse CreateSmartHouse();
	}
}
