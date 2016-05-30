using System;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes
{
	public class SmartHouse : DbContext, ISmartHouse
	{
		public DbSet<Fridge> Fridges { get; set; }
		public DbSet<SmartLamp> Lamps { get; set; }
		public DbSet<Clock> Clocks { get; set; }
		public DbSet<Dimmer> Dimmers { get; set; }

		static SmartHouse()
		{
			Database.SetInitializer<SmartHouse>(new SmartHouseDBInitializer());
		}

		public SmartHouse() { }

		public List<ISmartDevice> Devices
		{
			get
			{
				List<ISmartDevice> list = new List<ISmartDevice>();
				var frList = Fridges.Include(d => d.Dimmer);
				foreach (ISmartDevice dev in frList)
				{
					list.Add(dev);
				}
				var lpList = Lamps.Include(d => d.Dimmer);
				foreach (ISmartDevice dev in lpList)
				{
					list.Add(dev);
				}
				foreach (ISmartDevice dev in Clocks)
				{
					list.Add(dev);
				}
				return list;
			}
		}

		public virtual int Count
		{
			get
			{
				return Devices.Count();
			}
		}

		public ISmartDevice this[int Id]
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public virtual ISmartDevice this[string name]
		{
			get
			{
				return Devices.Where(d => d.Name == name).FirstOrDefault();
			}
		}

		public virtual bool AddDevice(ISmartDevice device)
		{
			bool result = device != null;

			if (result)
			{
				if (this[device.Name] == null)
				{
					//Devices.Add(device);
					if (device is Fridge)
					{
						Dimmers.Add((device as Fridge).Thermostat as Dimmer);
						Fridges.Add(device as Fridge);
					}
					if (device is SmartLamp)
					{
						Dimmers.Add((device as SmartLamp).Regulator as Dimmer);
						Lamps.Add(device as SmartLamp);
					}
					if (device is Clock)
					{
						Clocks.Add(device as Clock);
					}
					device.Parent = this;
					SaveChanges();
				}
				else
				{
					result = false;
				}
			}

			return result;
		}

		public virtual void RemoveDevice(string name)
		{
			ISmartDevice dev = Devices.Where(d => d.Name == name).FirstOrDefault();
			if (dev is Fridge)
			{
				var d = Fridges.Find((dev as IDbItem).Id);
				Fridges.Remove(d);
			}

			if (dev is SmartLamp)
			{
				var d = Lamps.Find((dev as IDbItem).Id);
				Lamps.Remove(d);
			}

			if (dev is Clock)
			{
				var d = Clocks.Find((dev as IDbItem).Id);
				Clocks.Remove(d);
			}

			SaveChanges();
		}

		public IEnumerator<ISmartDevice> GetEnumerator()
		{
			return Devices.ToList().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Devices.ToList().GetEnumerator();
		}
	}
}