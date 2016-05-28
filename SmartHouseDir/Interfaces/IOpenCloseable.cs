namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface IOpenCloseable
	{
		bool IsOpened { get; set; }

		void Open();
		void Close();
	}
}