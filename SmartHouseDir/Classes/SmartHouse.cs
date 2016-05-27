using System;
using System.Data.Entity;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes
{
	[Serializable]
	public class SmartHouse : DbContext, ISmartHouse
	{
		public DbSet<Fridge> Fridges { get; set; }
		public DbSet<SmartLamp> Lamps { get; set; }
		public DbSet<Clock> Clocks { get; set; }

		public SmartHouse() { }

		protected List<ISmartDevice> Devices
		{
			get
			{
				List<ISmartDevice> list = new List<ISmartDevice>();
				foreach (ISmartDevice dev in Fridges)
				{
					list.Add(dev);
				}
				foreach (ISmartDevice dev in Lamps)
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
						Fridges.Add(device as Fridge);
					}
					if (device is SmartLamp)
					{
						Lamps.Add(device as SmartLamp);
					}
					if (device is Clock)
					{
						Clocks.Add(device as Clock);
					}
					device.Parent = this;
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
			Devices.Remove(dev);
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