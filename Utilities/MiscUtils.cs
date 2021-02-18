using System;
using System.Collections.Generic;
using System.Linq;

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
	}
}
