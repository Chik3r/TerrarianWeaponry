using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using TerrarianWeaponry.DataLoading.Data.Parts;

namespace TerrarianWeaponry.DataLoading.Data
{
	public class PickaxeTool : BaseTool
	{
		public override string ToolName => "Pickaxe";

		public override List<Type> ToolParts => new List<Type>
		{
			typeof(PickaxeHeadPart),
			typeof(ToolRodPart)
		};

		public PickaxeTool(ItemInfo info, Texture2D texture, List<BaseMaterial> materials) : base(info, texture, materials) { }

		public override void SafeSetDefaults()
		{
			item.useStyle = ItemUseStyleID.SwingThrow;
		}
	}
}
