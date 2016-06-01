using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorkSmartHouse.SmartHouseDir.Interfaces
{
	public interface IAdjustable<T>
	{
		T CurrentLevel { get; set; }
		T Max { get; set; }
		T Min { get; set; }
		int Step { get; set; }

		void Increase();
		void Decrease();
	}
}