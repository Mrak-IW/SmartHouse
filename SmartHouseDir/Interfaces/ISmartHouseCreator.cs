using System;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface ISmartHouseCreator
	{
		ISmartDevice CreateDevice(string typename, string deviceName);
		ISmartHouse CreateSmartHouse();
		Type GetTypeByName(string name);
		Type SmartHouseType { get; }
	}
}
