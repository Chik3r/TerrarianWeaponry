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
		/// The field or property has a better value when higher, and it's type is <see cref="float"/>. An example is: knockback
		/// </summary>
		MultiplyFloat,

		/// <summary>
		/// The field or property has a better value when higher, and it's type is <see cref="int"/>. An example is: damage
		/// </summary>
		MultiplyInt,

		/// <summary>
		/// The field or property has a better value when lower, and it's type is <see cref="float"/>.
		/// </summary>
		DivideFloat,

		/// <summary>
		/// The field or property has a better value when lower, and it's type is <see cref="int"/>. An example is: useTime
		/// </summary>
		DivideInt,

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
