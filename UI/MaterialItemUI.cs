using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;

namespace TerrarianWeaponry.UI
{
	public class MaterialItemUI : UIElement
	{
		private ItemSlotWrapper _materialSlot;
		private UIText _materialName;
		private UIPanel _panel;

		private BaseMaterial _material;

		public MaterialItemUI(BaseMaterial material)
		{
			_material = material;

			_panel = new UIPanel
			{
				Height = new StyleDimension(80, 0),
				Width = new StyleDimension(200, 0),
			};

			_materialName = new UIText(_material.MaterialName)
			{
				Width = new StyleDimension(80, 0),
				Height = new StyleDimension(35, 0f),
				HAlign = 0f,
				VAlign = .5f
			};

			_materialSlot = new ItemSlotWrapper(ItemSlot.Context.CraftingMaterial)
			{
				HAlign = 1f,
				VAlign = .9f
			};

			var customItem = new Item();
			customItem.SetDefaults(Terraria.ID.ItemID.GoldBar);
			_materialSlot.Item = customItem;

			_panel.Append(_materialName);
			_panel.Append(_materialSlot);

			Append(_panel);
		}

		//public override void OnInitialize()
		//{
			
		//}

		//public override void Draw(SpriteBatch spriteBatch)
		//{
		//	base.Draw(spriteBatch);
		//	_panel.Draw(spriteBatch);
		//	Main.NewText("C");
		//}

		//protected override void DrawSelf(SpriteBatch spriteBatch)
		//{
		//	base.DrawSelf(spriteBatch);
		//	Main.NewText("B");
		//}

		//protected override void DrawChildren(SpriteBatch spriteBatch)
		//{
		//	base.DrawChildren(spriteBatch);
		//	Main.NewText("A");
		//}
	}
}
