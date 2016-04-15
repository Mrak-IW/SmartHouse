using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HomeWorkSmartHouse.ExtensionClasses
{
	public static class ExtString
	{
		public static string ToUpperFirstLetter(this string str)
		{
			return str.Substring(0, 1).ToUpper() + str.Substring(1);
		}
	}
}
