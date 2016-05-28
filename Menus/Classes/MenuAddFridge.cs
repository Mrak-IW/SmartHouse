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
	public class MenuAddFridge : Menu
	{
		const string usageHelp = "<имя_холодильника> [maxTemp [minTemp [step]]]";
		const string description = "Добавить в систему холодильник";
		const string name = "fr";

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

			int max = 5;
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
				min = -5;
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
				step = (max - min) / 10;
			}

			try
			{
				IAdjustable<int> dimmer = new Dimmer(max, min, step);
				dimmer.CurrentLevel = (dimmer.Max - dimmer.Min) / 2 + dimmer.Min;

				ISmartDevice dev = new Fridge(devName, dimmer);
				sh.AddDevice(dev);
			}
			catch (Exception e)
			{
				output = e.Message;
				return false;
			}

			return true;
		}
	}
}