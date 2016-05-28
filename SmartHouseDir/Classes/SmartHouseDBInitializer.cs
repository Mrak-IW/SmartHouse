using HomeWorkSmartHouse.SmartHouseDir.Classes;
using HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes
{
	class SmartHouseDBInitializer: CreateDatabaseIfNotExists<SmartHouse>
	{
		protected override void Seed(SmartHouse context)
		{
			ISmartHouse sh = context;
			ISmartHouseCreator shc = Program.GetManufacture(Assembly.Load("SmartHouse"));
			ISmartDevice dev;
			IBrightable ibri;
			IHaveThermostat iterm;

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
	}
}