using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWorkSmartHouse.Menus.Classes;
using HomeWorkSmartHouse.Menus.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using HomeWorkSmartHouse.SmartHouseDir.Classes;
using System.Data.Entity;

namespace HomeWorkSmartHouse
{
	class Program
	{
		const string storageFileDefault = "SmartHouse.bin";

		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

			SmartHouse sh = new SmartHouse();

			IEnumerable<ISmartDevice> devs = sh.Devices;

			CommandMenu cm = new CommandMenu(sh);
			IMenu add = new MenuAdd();
			IMenu bri = new MenuBrightness();
			IMenu temp = new MenuTemperature();

			cm.AddSubmenu(add);
			cm.AddSubmenu(new MenuRemove());
			cm.AddSubmenu(new MenuOn());
			cm.AddSubmenu(new MenuOff());
			cm.AddSubmenu(new MenuBreak());
			cm.AddSubmenu(new MenuRepare());
			cm.AddSubmenu(bri);
			cm.AddSubmenu(temp);
			cm.AddSubmenu(new MenuOpen());
			cm.AddSubmenu(new MenuClose());

			add.AddSubmenu(new MenuAddLamp());
			add.AddSubmenu(new MenuAddFridge());
			add.AddSubmenu(new MenuAddClock());

			bri.AddSubmenu(new MenuBrightnessDecrease());
			bri.AddSubmenu(new MenuBrightnessIncrease());
			bri.AddSubmenu(new MenuBrightnessSet());

			temp.AddSubmenu(new MenuTemperatureIncrease());
			temp.AddSubmenu(new MenuTemperatureDecrease());
			temp.AddSubmenu(new MenuTemperatureSet());

			cm.Show();

			sh.SaveChanges();
		}

		public static ISmartHouseCreator GetManufacture(Assembly smartHouseAssembly)
		{
			ISmartHouseCreator shc = null;
			Type shcType = null;

			var res = from t in smartHouseAssembly.GetTypes()
					  where t.GetInterfaces().Contains(typeof(ISmartHouseCreator))
					  select t;

			shcType = res.FirstOrDefault();

			if (shcType != null)
			{
				object[] constructorArgs = new object[1];
				constructorArgs[0] = smartHouseAssembly;

				shc = Activator.CreateInstance(shcType, constructorArgs) as ISmartHouseCreator;
			}

			return shc;
		}
	}
}
