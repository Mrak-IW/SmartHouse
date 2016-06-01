using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.Menus.Abstracts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;

namespace HomeWorkSmartHouse.Menus.Classes
{
	class CommandMenu : Menu
	{
		ISmartHouse sh;

		const string description = "Меню умного дома";
		const string usageHelp = "<команда> [аргументы команды]\n\n" + EXIT + "\tвыход";
		const string name = "sh>";

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

		public CommandMenu(ISmartHouse sh)
		{
			this.sh = sh;
		}

		public void Show()
		{
			const int cmdIndex = 0;

			string ans = null;
			string output = null;
			bool result = true;
			string[] args;
			string cmd;
			while (true)
			{
				ShowState();
				if (ans != null)
				{
					Console.WriteLine(string.Format("{0} {1}\n", Name, ans));
				}
				if (output != null)
				{
					Console.WriteLine(output);
				}
				Console.Write("\n{0} ", this.Name);
				ans = Console.ReadLine();
				args = ans.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

				if (args != null && args.Length > cmdIndex)
				{
					cmd = args[cmdIndex];
					if (cmd == EXIT)
					{
						break;
					}
				}

				result = Call(sh, out output, args);
				if (!result)
				{
					if (output != null)
					{
						output = string.Join("\n\n", output, this.UsageHelp);
					}
					else
					{
						output = this.UsageHelp;
					}
				}
			}

			Console.WriteLine("Завершение работы программы");
		}

		private void ShowState()
		{
			Console.Clear();
			Console.WriteLine("Устройств в системе {0}:", sh.Count);
			foreach(ISmartDevice dev in sh)
			{
				Console.WriteLine(dev);
			}
			Console.WriteLine();
		}
	}
}
