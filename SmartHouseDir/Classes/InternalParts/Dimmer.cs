using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts
{
	public class Dimmer : IAdjustable<int>
	{
		private int currentLevel;
		private readonly int min;
		private readonly int max;
		private readonly int step;

		public Dimmer(int max, int min, int step)
		{
			if (max < min)
			{
				throw new IndexOutOfRangeException("Попытка задать некорректные границы диапазона значений ");
			}
			if (step < 0 || step > max - min)
			{
				throw new IndexOutOfRangeException("Попытка задать некорректный шаг изменения");
			}
			this.min = min;
			this.max = max;
			this.step = step;
			this.CurrentLevel = this.Max;
		}

		public virtual int CurrentLevel
		{
			get
			{
				return currentLevel;
			}
			set
			{
				if (value >= Min && value <= Max)
				{
					currentLevel = value;
				}
			}
		}

		public virtual int Max
		{
			get
			{
				return max;
			}
		}
		public virtual int Min
		{
			get
			{
				return min;
			}
		}

		public int Step
		{
			get
			{
				return step;
			}
		}

		public virtual void Decrease()
		{
			CurrentLevel -= step;
		}
		public virtual void Increase()
		{
			CurrentLevel += step;
		}
	}

}
