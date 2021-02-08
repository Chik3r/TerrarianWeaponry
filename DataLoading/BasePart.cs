using System;
using System.Collections.Generic;

namespace TerrarianWeaponry.DataLoading
{
	public abstract class BasePart
	{
		/// <summary>
		/// Override this to set the name of the part, ex: a tool rod would set this to "Tool Rod"
		/// </summary>
		public abstract string PartName { get; }

		/// <summary>
		/// Override this to set the list of materials of this part.
		/// <br/> Make sure all types extend <see cref="BaseMaterial"/>
		/// </summary>
		internal abstract List<Type> ValidMaterials { get; }

		internal IEnumerable<BaseMaterial> LoadMaterials()
		{
			var baseType = typeof(BaseMaterial);

			foreach (Type toolPartType in ValidMaterials)
			{
				if (!toolPartType.IsAssignableFrom(baseType))
					throw new ArgumentException("All types inside ValidMaterials should extend BaseMaterial");

				yield return (BaseMaterial) Activator.CreateInstance(toolPartType);
			}
		}
	}
}
