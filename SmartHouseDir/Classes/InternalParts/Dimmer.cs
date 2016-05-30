using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWorkSmartHouse.SmartHouseDir.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeWorkSmartHouse.SmartHouseDir.Classes.InternalParts
{
	[Serializable]
	public class Dimmer : IAdjustable<int>, IDbItem
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

		public int Id { get; set; }

		public Fridge Fridge { get; set; }

		public SmartLamp SmartLamp { get; set; }

		//Да, это костыль. Но я пытался прикрутить EntityFramework к уже готовой модели, минимально изменяя логику её работы.
		//Если использовать свойство с проверкой значения, то значение из базы восстанавливается некорректно.
		[Column("CurrentLevel")]
		public virtual int CurrentLevelUnsafe
		{
			get
			{
				return currentLevel;
			}
			set
			{
				currentLevel = value;
			}
		}

		[NotMapped]
		public virtual int CurrentLevel
		{
			get
			{
				return currentLevel;
			}
			set
			{
				if (value < Min)
				{
					currentLevel = Min;
				}
				else if (value > Max)
				{
					currentLevel = Max;
				}
				else
				{
					currentLevel = value;
				}
				currentLevel = value;
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
				CurrentLevel = CurrentLevel;
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
				CurrentLevel = CurrentLevel;
			}
		}

		public virtual int Step
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
