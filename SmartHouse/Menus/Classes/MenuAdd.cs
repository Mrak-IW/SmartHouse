using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.Menus.Abstracts;

namespace HomeWorkSmartHouse.Menus.Classes
{
	public class MenuAdd : Menu
	{
		const string usageHelp = "<тип_устройства> <имя_устройства> [специфические параметры устройства]\n\nТипы устройств:";
		const string description = "Добавление устройств в систему";
		const string name = "add";

		public override string Name
		{
			get
			{
				return name;
			}
		}

		public override string UsageHelpShort
		{
			get
			{
				return usageHelp;
			}
		}

		public override string Description
		{
			get
			{
				return description;
			}
		}
	}
}