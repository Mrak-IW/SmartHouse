using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HomeWorkSmartHouse.SmartHouseDir.Enums;


namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface ISmartDevice
	{
		[Key]
		string Name { get; set; }
		[NotMapped]
		string DeviceType { get; }
		EPowerState State { get; set; }
		ISmartHouse Parent { get; set; }

		void On();
		void Off();
		void Break();
	}
}