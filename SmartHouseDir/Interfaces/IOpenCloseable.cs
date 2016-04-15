using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface IOpenCloseable
	{
		bool IsOpened { get; set; }

		void Open();
		void Close();
	}
}