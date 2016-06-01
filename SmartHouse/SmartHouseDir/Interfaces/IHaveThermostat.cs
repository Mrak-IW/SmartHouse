namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface IHaveThermostat
	{
		IAdjustable<int> Thermostat { get; set; }
		int Temperature { get; set; }
		int TempMax { get; set; }
		int TempMin { get; set; }
		int TempStep { get; set; }

		void IncreaseTemperature();
		void DecreaseTemperature();
	}
}