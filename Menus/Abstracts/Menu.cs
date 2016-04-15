using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.Menus.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;

namespace HomeWorkSmartHouse.Menus.Abstracts
{
	public abstract class Menu : IMenu, IEnumerable<IMenu>
	{
		protected const string EXIT = "exit";
		protected const string MISSING_ARGS = "Недостаточно аргументов для команды ";
		protected const string DEV_NOT_FOUND = "не найдено";

		IDictionary<string, IMenu> submenus = new Dictionary<string, IMenu>();

		public abstract string Description { get; }
		public abstract string UsageHelpShort { get; }
		public abstract string Name { get; }

		public virtual string UsageHelp
		{
			get
			{
				string result = string.Format("-=[ {0} ]=-\n\n{1} {2}", Description, FullName, UsageHelpShort);
				foreach (IMenu item in this)
				{
					result = string.Format("{0}\n{1}\t{2}", result, item.Name, item);
				}
				return result;
			}
		}

		public string FullName
		{
			get
			{
				if (Parent != null && Parent.Parent != null)
				{
					return string.Join(" ", Parent.FullName, this.Name);
				}
				else
					return this.Name;
			}
		}

		public IMenu Parent { get; set; }

		public IMenu this[string submenuName]
		{
			get
			{
				if (submenus.ContainsKey(submenuName))
				{
					return submenus[submenuName];
				}
				else
				{
					return null;
				}
			}
		}

		public virtual bool Call(ISmartHouse sh, out string output, params string[] args)
		{
			output = null;

			if (args == null || args.Length == 0)
			{
				output = MISSING_ARGS + Name;
				return false;
			}

			bool result = true;
			string submenuName = args[0];

			IMenu sm;
			if (!ContainsSubmenu(submenuName))
			{
				output = string.Format("{0} не обладает вложенной командой {1}", Name, submenuName);
				result = false;
			}
			else
			{
				sm = this[submenuName];
				if (!sm.Call(sh, out output, Last(args, 1)))
				{
					if (output != null)
					{
						output = string.Join("\n\n", output, sm.UsageHelp);
					}
					else
					{
						output = sm.UsageHelp;
					}
					result = true;
				}
			}

			return result;
		}

		protected string[] Last(string[] array, int startIndex)
		{
			string[] result = null;

			if (array != null && array.Length > startIndex)
			{
				result = new string[array.Length - startIndex];
				for (int i = 0; i < result.Length; i++)
				{
					result[i] = array[i + startIndex];
				}
			}

			return result;
		}

		public override string ToString()
		{
			return Description;
		}

		public bool AddSubmenu(IMenu submenu)
		{
			bool result = true;
			if (!ContainsSubmenu(submenu.Name))
			{
				submenus.Add(submenu.Name, submenu);
				submenu.Parent = this;
			}
			else
			{
				result = false;
			}
			return result;
		}

		public bool ContainsSubmenu(string name)
		{
			return submenus.ContainsKey(name);
		}

		public IEnumerator<IMenu> GetEnumerator()
		{
			return submenus.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return submenus.Values.GetEnumerator();
		}
	}
}