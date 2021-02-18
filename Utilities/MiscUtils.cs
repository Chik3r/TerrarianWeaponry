using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;

namespace TerrarianWeaponry.Utilities
{
	public static class MiscUtils
	{
		internal static IEnumerable<T> GetTypesExtendingT<T>(params object[] constructorArgs)
		{
			// Get all types extending T
			var baseType = typeof(T);
			var childTypes = TerrarianWeaponry.Instance.Code.GetTypes()
				.Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract);

			// And return an IEnumerable from an instance of the types
			foreach (Type childType in childTypes)
				yield return (T)Activator.CreateInstance(childType, constructorArgs);
		}

		public static bool SafeConsume(this Item item, int amount = 1)
		{
			if (item.stack < amount)
				return false;

			item.stack -= amount;

			if (item.stack == 0)
				item.TurnToAir();

			return true;
		}
	}
}
