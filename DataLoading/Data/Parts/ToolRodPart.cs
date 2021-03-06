﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TerrarianWeaponry.DataLoading.Data.Parts
{
	public class ToolRodPart : BasePart
	{
		public override string PartName => "Tool Rod";

		public override string Description => "Some description, idk";

		internal override List<(BaseMaterial material, TextureInfo textureInfo)> ValidMaterials => new List<(BaseMaterial material, TextureInfo textureInfo)>
		{
			(new TestModifierMaterial(), new TextureInfo("DataLoading/Data/Materials/Textures/TestModifierMaterial", new Point(21, 10))),
			(new TestBothMaterial(), new TextureInfo("DataLoading/Data/Materials/Textures/StoneStick", new Point(21, 10))),
		};

		public override int MaterialCost => 3;
	}
}
