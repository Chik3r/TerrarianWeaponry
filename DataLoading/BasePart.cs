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
		/// </summary>
		internal abstract List<BaseMaterial> ValidMaterials { get; }
	}
}
