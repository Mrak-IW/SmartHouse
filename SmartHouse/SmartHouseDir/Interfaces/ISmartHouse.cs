﻿using System.Collections.Generic;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface ISmartHouse: IEnumerable<ISmartDevice>
	{
		ISmartDevice this[string name] { get; }
		ISmartDevice this[int Id] { get; }
		IList<ISmartDevice> Devices { get; }

		int Count { get; }

		bool AddDevice(ISmartDevice device);
		void RemoveDevice(string name);
	}
}