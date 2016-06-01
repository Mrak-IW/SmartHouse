using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.Menus.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;

namespace HomeWorkSmartHouse.Menus.Classes
{
	public class MenuTemperatureDecrease : Menu
	{
		const string usageHelp = "<имя_устройства_1> [ .. <имя_устройства_N>]";
		const string description = "Уменьшить температуру";
		const string name = "do";

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

		public override bool Call(ISmartHouse sh, out string output, params string[] args)
		{
			bool result = true;
			output = null;
			bool devFound;
			string action;
			ISmartDevice dev;

			if (args != null && args.Length > 0)
			{
				for (int i = 0; i < args.Length; i++)
				{
					dev = sh[args[i]];
					devFound = dev != null;

					if (devFound)
					{
						if (dev is IHaveThermostat)
						{
							(dev as IHaveThermostat).DecreaseTemperature();
							action = "стало холоднее";
						}
						else
						{
							action = "не имеет настроек температуры";
						}
					}
					else
					{
						action = DEV_NOT_FOUND;
					}

					output = string.Format("{2} Устройство {0} {1}", args[i], action, (output != null ? output + "\n" : ""));
				}
			}
			else
			{
				output = MISSING_ARGS + Name;
				result = false;
			}

			return result;
		}
	}
}