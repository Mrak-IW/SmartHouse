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

namespace HomeWorkSmartHouse
{
	class Program
	{
		const string storageFileDefault = "SmartHouse.bin";

		static void Main(string[] args)
		{
			Assembly a = Assembly.Load("SmartHouse");
			ISmartHouseCreator shc = GetManufacture(a);
			ISmartDevice dev;
			IBrightable ibri;
			IHaveThermostat iterm;

			ISmartHouse sh = null;

			string storageFile = null;
			FileInfo storageFileInfo = null;

			if (args.Length > 0)
			{
				storageFile = args[0];
				try
				{
					storageFileInfo = new FileInfo(storageFile);
				}
				catch
				{
					storageFileInfo = null;
				}
			}

			if (storageFileInfo == null)
			{
				storageFile = storageFileDefault;
				storageFileInfo = new FileInfo(storageFile);
			}

			if (storageFileInfo.Exists)
			{
				using (FileStream fs = new FileStream(storageFile, FileMode.Open))
				{
					BinaryFormatter bf = new BinaryFormatter();
					try
					{
						sh = bf.Deserialize(fs) as ISmartHouse;
					}
					catch
					{
						sh = null;
					}
				}
			}

			if (sh == null)
			{
				sh = shc.CreateSmartHouse();

				dev = shc.CreateDevice("SmartLamp", "l1");

				ibri = dev as IBrightable;
				ibri.BrightnessMax = 100;
				ibri.BrightnessMin = 10;
				ibri.BrightnessStep = 10;
				sh.AddDevice(dev);

				dev = shc.CreateDevice("SmartLamp", "l2");

				ibri = dev as IBrightable;
				ibri.BrightnessMax = 100;
				ibri.BrightnessMin = 10;
				ibri.BrightnessStep = 15;
				sh.AddDevice(dev);

				dev = shc.CreateDevice("Fridge", "fr1");

				iterm = dev as IHaveThermostat;
				iterm.TempMax = 0;
				iterm.TempMin = -5;
				iterm.TempStep = 1;
				dev.On();
				iterm.DecreaseTemperature();
				sh.AddDevice(dev);

				dev = shc.CreateDevice("Clock", "clk1");

				dev.On();
				sh.AddDevice(dev);
			}
			//ISmartHouse sh = new SmartHouseDir.Classes.SmartHouse();
			//sh.AddDevice(new SmartLamp("l1", new Dimmer(100, 10, 10)));
			//sh.AddDevice(new SmartLamp("l2", new Dimmer(100, 10, 15)));
			//sh.AddDevice(new Fridge("fr", new Dimmer(0, -5, 1)));
			//sh.AddDevice(new Clock("clk"));

			//sh["fr"].On();
			//sh["clk"].On();
			//(sh["fr"] as IHaveThermostat).DecreaseTemperature();

			//SmartLamp testDev = shc.CreateDevice("SmartLamp", "fr2") as SmartLamp;
			//testDev.BrightnessMax = 10;
			//testDev.BrightnessMin = -10;
			//testDev.Step = 10;
			//testDev.On();
			//sh.AddDevice(testDev);

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

			using (FileStream fs = new FileStream(storageFile, FileMode.Create))
			{
				BinaryFormatter bf = new BinaryFormatter();
				bf.Serialize(fs, sh);
			}
		}

		static ISmartHouseCreator GetManufacture(Assembly smartHouseAssembly)
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
