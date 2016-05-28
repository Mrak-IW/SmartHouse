using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using HomeWorkSmartHouse.SmartHouseDir.Enums;

namespace HomeWorkSmartHouse.SmartHouseDir.Abstracts
{
	[Serializable]
	public abstract class SmartDevice : ISmartDevice, IDbItem
	{
		private string name;
		private EPowerState state;
		private ISmartHouse parent;

		public SmartDevice()
		{
			this.Name = null;
		}

		public SmartDevice(string name)
		{
			this.Name = name;
		}

		public int Id { get; set; }

		public virtual string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public virtual EPowerState State
		{
			get
			{
				return state;
			}

			set
			{
				state = value;
			}
		}

		public virtual ISmartHouse Parent
		{
			get
			{
				return parent;
			}

			set
			{
				parent = value;
			}
		}

		[NotMapped]
		public abstract string DeviceType { get; }

		public virtual void Break()
		{
			State = EPowerState.Broken;
		}

		public virtual void Off()
		{
			if (State != EPowerState.Broken)
			{
				State = EPowerState.Off;
			}
		}

		public virtual void On()
		{
			if (State != EPowerState.Broken)
			{
				State = EPowerState.On;
			}
		}
	}
}