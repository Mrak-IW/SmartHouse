using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.Menus.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Classes;

namespace HomeWorkSmartHouse.Menus.Classes
{
	public class MenuAddClock : Menu
	{
		const string usageHelp = "<имя_часов>";
		const string description = "Добавить в систему часы";
		const string name = "clk";

		public override string Name
		{
			get
			{
				return name;
			}
		}

		public override string Description
		{
			get
			{
				return description;
			}
		}

		public override string UsageHelpShort
		{
			get
			{
				return usageHelp;
			}
		}

		public override bool Call(ISmartHouse sh, out string output, params string[] args)
		{
			output = null;

			if (args == null || args.Length != 1)
			{
				output = MISSING_ARGS + Name;
				return false;
			}
			else if (sh[args[0]] != null)
			{
				output = string.Format("Устройство с именем \"{0}\" уже есть в системе", args[0]);
				return false;
			}

			sh.AddDevice(new Clock(args[0]));

			return true;
		}
	}
}