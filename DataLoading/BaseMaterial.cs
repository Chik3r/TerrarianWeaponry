using Microsoft.Xna.Framework;

namespace TerrarianWeaponry.DataLoading
{
	public abstract class BaseMaterial
	{
		/// <summary>
		/// Override this to set the name of the material, ex: a pickaxe would set this to "Pickaxe"
		/// </summary>
		public abstract string MaterialName { get; }

		/// <summary>
		/// Override this to set the texture of the material
		/// </summary>
		public abstract string Texture { get; }

		/// <summary>
		/// Override this to set the origin point of the material's texture
		/// </summary>
		public abstract Point OriginPoint { get; }

		/// <summary>
		/// Override this to set the valid item types to create this material, ex: 3 (or ItemID.StoneBlock) for a stone block
		/// </summary>
		public abstract short[] MaterialTypes { get; }
		
		public virtual float? Modifier { get; }

		/// <summary>
		/// Override this to modify the item's info
		/// </summary>
		/// <param name="info">The item info to modify</param>
		/// <param name="modifier">The highest modifier of all parts</param>
		public virtual void ModifyStats(ref ItemInfo info, float modifier) { }
	}
}
