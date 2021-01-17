using System;

namespace TerrarianWeaponry.JsonData
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ModifierTypeAttribute : Attribute
	{
		public readonly ModifierTypes Type;

		public ModifierTypeAttribute(ModifierTypes type)
		{
			Type = type;
		}
	}

	public enum ModifierTypes
	{
		/// <summary>
		/// The field or property has a better value when higher. An example is: damage
		/// </summary>
		Multiply,

		/// <summary>
		/// The field or property has a better value when lower. An example is: useTime
		/// </summary>
		Divide,

		/// <summary>
		/// The field or property is a modifier
		/// </summary>
		Modifier,

		/// <summary>
		/// The field or property is not affected by modifiers
		/// </summary>
		None
	}
}
