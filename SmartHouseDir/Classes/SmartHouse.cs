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
		public DbSet<ISmartDevice> Devices { get; set; }

		public SmartHouse() { }

		public virtual int Count
		{
			get
			{
				return Devices.Count();
			}
		}

		public virtual ISmartDevice this[string name]
		{
			get
			{
				return Devices.Find(name);
			}
		}

		public virtual bool AddDevice(ISmartDevice device)
		{
			bool result = device != null;

			if (result)
			{
				if (this[device.Name] == null)
				{
					Devices.Add(device);
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
			ISmartDevice dev = Devices.Find(name);
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