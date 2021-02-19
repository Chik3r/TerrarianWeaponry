using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		/// <summary>
		/// Wraps the <paramref name="inputString"/> into a <see langword="string"/> <see langword="IEnumerable"/>
		/// </summary>
		/// <param name="inputString">The string to split</param>
		/// <param name="characterLimit">The character limit before making a new line</param>
		/// <returns>An IEnumerable where every item is a line of the split text</returns>
		public static IEnumerable<string> WrapText(string inputString, int characterLimit = 14)
		{
			string[] splitText = inputString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder sb = new StringBuilder();

			string line = "";
			foreach (string word in splitText)
			{
				if ((line + word).Length > characterLimit)
				{
					sb.AppendLine(line);
					line = "";
				}

				line += $"{word} ";
			}

			if (line.Length > 0)
				sb.AppendLine(line);

			return sb.ToString().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
		}
	}
}
