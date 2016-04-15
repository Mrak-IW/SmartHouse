using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeWorkSmartHouse.Menus.Classes;
using HomeWorkSmartHouse.Menus.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Classes;
using HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts;

namespace HomeWorkSmartHouse
{
	class Program
	{
		static void Main(string[] args)
		{
			ISmartHouse sh = new SmartHouseDir.Classes.SmartHouse();
			sh.AddDevice(new SmartLamp("l1", new Dimmer(100, 10, 10)));
			sh.AddDevice(new SmartLamp("l2", new Dimmer(100, 10, 15)));
			sh.AddDevice(new Fridge("fr", new Dimmer(0, -5, 1)));
			sh.AddDevice(new Clock("clk"));
			sh["fr"].On();
			sh["clk"].On();
			(sh["fr"] as IHaveThermostat).DecreaseTemperature();

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
		}
	}
}
