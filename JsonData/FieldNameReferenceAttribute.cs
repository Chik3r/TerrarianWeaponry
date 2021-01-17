using System;

namespace TerrarianWeaponry.JsonData
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class FieldNameReferenceAttribute : Attribute
	{
		public readonly string FieldName;

		public FieldNameReferenceAttribute(string fieldName)
		{
			FieldName = fieldName;
		}
	}
}
