using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;

namespace HomeWorkSmartHouse.Menus.Interfaces
{
	public interface IMenu
	{
		bool Call(ISmartHouse sh, out string output, params string[] args);

		IMenu this[string submenuName] { get; }

		string UsageHelp { get; }
		string Description { get; }
		string UsageHelpShort { get; }
		string Name { get; }
		string FullName { get; }

		bool AddSubmenu(IMenu submenu);
		bool ContainsSubmenu(string name);

		IMenu Parent { get; set; }
	}
}