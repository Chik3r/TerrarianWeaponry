using System;
using System.Linq;

namespace TerrarianWeaponry.DataLoading
{
	public abstract class BaseMaterial
	{
		/// <summary>
		/// Override this to set the name of the material, ex: stone would set this to "Stone"
		/// </summary>
		public abstract string MaterialName { get; }

		/// <summary>
		/// Override this to set the valid item types to create this material, ex: 3 (or ItemID.StoneBlock) for a stone block
		/// </summary>
		public abstract int[] MaterialTypes { get; }
		
		public virtual float? Modifier { get; }

		/// <summary>
		/// Override this to modify the item's info
		/// </summary>
		/// <param name="info">The item info to modify</param>
		/// <param name="modifier">The highest modifier of all parts</param>
		public virtual void ModifyStats(ref ItemInfo info, float modifier) { }

		public override bool Equals(object obj)
		{
			if (!(obj is BaseMaterial target))
				return false;

			if (Modifier != target.Modifier)
				return false;

			if (MaterialName != target.MaterialName)
				return false;

			if (!MaterialTypes.SequenceEqual(target.MaterialTypes))
				return false;

			return true;
		}

		public static bool operator ==(BaseMaterial a, BaseMaterial b)
		{
			return a?.Equals(b) ?? false;
		}

		public static bool operator !=(BaseMaterial a, BaseMaterial b)
		{
			return !(a == b);
		}

		public override int GetHashCode()
		{
			return Tuple.Create(MaterialName, MaterialTypes, Modifier).GetHashCode();
		}
	}
}
