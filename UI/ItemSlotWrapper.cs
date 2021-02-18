using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameInput;
using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	public class ItemSlotWrapper : UIElement
	{
		private Item _item;
		internal Item Item
		{
			get => _item;
			set
			{
				if (_item != value)
					OnItemChanged?.Invoke(value);

				_item = value;
			}
		}

		private readonly int _context;
		private readonly float _scale;
		private readonly bool _canInsert;
		internal Func<Item, bool> ValidItemFunc;
		internal Action<Item> OnItemChanged;

		public ItemSlotWrapper(int context = ItemSlot.Context.ChestItem, float scale = 1f, bool canInsert = true)
		{
			_context = context;
			_scale = scale;
			_canInsert = canInsert;
			Item = new Item();
			Item.SetDefaults();

			Width.Set(Main.inventoryBack9Texture.Width * scale, 0f);
			Height.Set(Main.inventoryBack9Texture.Height * scale, 0f);
		}

		protected override void DrawSelf(SpriteBatch spriteBatch)
		{
			float oldScale = Main.inventoryScale;
			Main.inventoryScale = _scale;
			Rectangle rect = GetDimensions().ToRectangle();
			
			Item tmpItem = Item;

			if (ContainsPoint(Main.MouseScreen) && !PlayerInput.IgnoreMouseInterface)
			{
				Main.LocalPlayer.mouseInterface = true;
				if (ValidItemFunc == null || ValidItemFunc(Main.mouseItem))
				{
					// Handle handles all the click and hover actions based on the context.

					// If the slot doesn't allow insertion, check that there's no held mouse item before handling the slot
					if (!_canInsert && Main.mouseItem.IsAir)
						ItemSlot.Handle(ref tmpItem, _context);
					// If it does allow insertion, handle the slot normally
					else if (_canInsert)
						ItemSlot.Handle(ref tmpItem, _context);

					Item = tmpItem;
				}
			}

			// Draw draws the slot itself and Item. Depending on context, the color will change, as will drawing other things like stack counts.
			tmpItem = Item;
			ItemSlot.Draw(spriteBatch, ref tmpItem, _context, rect.TopLeft());
			Item = tmpItem;
			Main.inventoryScale = oldScale;
		}
	}
}
