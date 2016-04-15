using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.Menus.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts;
using HomeWorkSmartHouse.SmartHouseDir.Classes;

namespace HomeWorkSmartHouse.Menus.Classes
{
	public class MenuAddLamp : Menu
	{
		const string usageHelp = "<имя_лампы> [maxBrightness [minBrightness [step]]]";
		const string description = "Добавить в систему умную лампу";
		const string name = "lp";

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
			bool result = true;
			output = null;

			int max = 100;
			int min = 0;
			int step = 0;
			int i = 0;
			string devName;

			if (args == null || args.Length == 0 || args.Length > 4)
			{
				output = MISSING_ARGS + Name;
				return false;
			}
			else if (sh[args[i]] != null)
			{
				output = string.Format("Устройство с именем \"{0}\" уже есть в системе", args[i]);
				return false;
			}

			devName = args[i];

			if (args.Length > ++i)
			{
				if (!int.TryParse(args[i], out max))
				{
					output = string.Format("Второй аргумент \"{0}\" не является целым числом", args[i]);
					return false;
				}
			}

			if (args.Length > ++i)
			{
				if (!int.TryParse(args[i], out min))
				{
					output = string.Format("Третий аргумент \"{0}\" не является целым числом", args[i]);
					return false;
				}
			}
			else
			{
				min = max / 10;
			}

			if (args.Length > ++i)
			{
				if (!int.TryParse(args[i], out step))
				{
					output = string.Format("Четвёртый аргумент \"{0}\" не является целым числом", args[i]);
					return false;
				}
			}
			else
			{
				step = max / 10;
			}

			try
			{
				IAdjustable<int> dimmer = new Dimmer(max, min, step);
				ISmartDevice dev = new SmartLamp(devName, dimmer);

				sh.AddDevice(dev);
			}
			catch (Exception e)
			{
				output = e.Message;
				return false;
			}

			return result;
		}
	}
}