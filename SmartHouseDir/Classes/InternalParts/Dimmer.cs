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
		private int min;
		private int max;
		private int step;

		public Dimmer()
		{ }

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

			set
			{
				if (value < Min)
				{
					Min = value;
				}
				max = value;
			}
		}

		public virtual int Min
		{
			get
			{
				return min;
			}

			set
			{
				if (value > Max)
				{
					Max = value;
				}
				min = value;
			}
		}

		public int Step
		{
			get
			{
				if (step > max - min)
				{
					step = max - min;
				}
				return step;
			}

			set
			{
				if (value <= max - min)
				{
					step = value;
				}
				else
				{
					step = max - min;
				}
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
