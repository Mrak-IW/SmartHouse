using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface ISmartDevice
	{
		string Name { get; set; }
		string DeviceType { get; }
		string DeviceTypeSystem { get; }	
		EPowerState State { get; set; }
		ISmartHouse Parent { get; set; }

		void On();
		void Off();
		void Break();
	}
}