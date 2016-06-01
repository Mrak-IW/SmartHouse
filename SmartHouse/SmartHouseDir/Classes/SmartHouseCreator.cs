using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes
{
	public class SmartHouseCreator : ISmartHouseCreator
	{
		public Assembly SmartHouseAssembly { get; set; }

		public SmartHouseCreator(Assembly smartHouseAssembly)
		{
			this.SmartHouseAssembly = smartHouseAssembly;
		}

		public ISmartHouse CreateSmartHouse()
		{
			ISmartHouse sh = null;

			var res = from t in SmartHouseAssembly.GetTypes()
					  where t.GetInterfaces().Contains(typeof(ISmartHouse))
					  select t;

			Type shType = res.FirstOrDefault();

			if (shType != null)
			{
				sh = Activator.CreateInstance(shType) as ISmartHouse;
			}

			return sh;
		}

		public ISmartDevice CreateDevice(string typename, string deviceName)
		{
			ISmartDevice dev = null;

			Type devType = null;

			foreach (Type t in SmartHouseAssembly.GetTypes())
			{
				if (t.Name == typename)
				{
					devType = t;
					break;
				}
			}

			if (devType != null)
			{

				object[] constructorParams = new object[1];
				constructorParams[0] = deviceName;

				dev = Activator.CreateInstance(devType, constructorParams) as ISmartDevice;

				if (dev != null)
				{
					if (dev is IBrightable)
					{
						IBrightable currentDev = dev as IBrightable;

						Type dimmerIface = typeof(IAdjustable<int>);
						Type dimmerType = null;

						foreach (Type t in SmartHouseAssembly.GetTypes())
						{
							if (t.GetInterfaces().Contains(dimmerIface))
							{
								dimmerType = t;
								break;
							}
						}

						if (dimmerType == null)
						{
							throw new Exception("В сборке отстутствует класс, который можно использовать в качестве диммера.");
						}

						IAdjustable<int> dimmer = Activator.CreateInstance(dimmerType) as IAdjustable<int>;
						if (dimmer == null)
						{
							throw new Exception(string.Format("Не удалось создать диммер класса \"{0}\"", dimmerType.Name));
						}

						currentDev.Regulator = dimmer;
					}

					if (dev is IHaveThermostat)
					{
						IHaveThermostat currentDev = dev as IHaveThermostat;

						Type thermostatIface = typeof(IAdjustable<int>);
						Type thermostatType = null;

						foreach (Type t in SmartHouseAssembly.GetTypes())
						{
							if (t.GetInterfaces().Contains(thermostatIface))
							{
								thermostatType = t;
								break;
							}
						}

						if (thermostatType == null)
						{
							throw new Exception("В сборке отстутствует класс, который можно использовать в качестве термостата.");
						}

						IAdjustable<int> thermostat = Activator.CreateInstance(thermostatType) as IAdjustable<int>;
						if (thermostat == null)
						{
							throw new Exception(string.Format("Не удалось создать диммер класса \"{0}\"", thermostatType.Name));
						}

						currentDev.Thermostat = thermostat;
					}
				}
			}

			return dev;
		}

		public Type GetTypeByName(string name)
		{
			var res = from t in SmartHouseAssembly.GetTypes()
					  where t.Name==name
					  select t;
			return res.FirstOrDefault();
		}

		public Type SmartHouseType
		{
			get
			{
				var res = from t in SmartHouseAssembly.GetTypes()
						  where t.GetInterfaces().Contains(typeof(ISmartHouse))
						  select t;
				return res.FirstOrDefault();
			}
		}
	}
}
