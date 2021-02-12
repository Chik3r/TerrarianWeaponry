using System.Collections.Generic;

namespace TerrarianWeaponry.DataLoading
{
	public abstract class BasePart
	{
		/// <summary>
		/// Override this to set the name of the part, ex: a tool rod would set this to "Tool Rod"
		/// </summary>
		public abstract string PartName { get; }

		public virtual string Description => "";

		/// <summary>
		/// Override this to set the list of materials of this part.
		/// <br/>The "material" of the tuple should be set to the material this part can be crafted from
		/// <br/>The "texture" of the tuple should be set to the texture of the "material"
		/// </summary>
		internal abstract List<(BaseMaterial material, TextureInfo textureInfo)> ValidMaterials { get; }
	}
}
