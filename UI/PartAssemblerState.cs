using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;
using TerrarianWeaponry.Utilities;

namespace TerrarianWeaponry.UI
{
	internal partial class PartAssemblerState : UIState
	{
		private TabPanel _tabPanel;
		private UIPanel _partsPanel;
		private UIList _partsList;

		private ItemSlotWrapper _inputSlot;
		private UITextPanel<string> _createBtn;
		private ItemSlotWrapper _outputSlot;

		private UIPanel _partInfoPanel;
		private UIImage _partInfoImage;
		private UIText _partInfoName;
		private UIList _partInfoDescription;

		private BasePart _selectedPart;

		public override void OnInitialize()
		{
			// Create the main panel
			CreateMainPanel();

			// Create panel containing a list of parts
			CreatePartListPanel();

			// Create the part assembler
			CreatePartAssembler();

			// Create panel for the part info
			CreatePartInfoPanel();
		}

		private void AddPartsToList()
		{
			foreach (BasePart basePart in MiscUtils.GetTypesExtendingT<BasePart>())
			{
				_partsList.Add(new UITextData<BasePart>(basePart.PartName)
				{
					extraData = basePart,
					OnClicked = OnClickPartText
				});
			}
		}

		public override void OnDeactivate()
		{
			DeactivateSlot(_inputSlot);
			DeactivateSlot(_outputSlot);
		}

		private void DeactivateSlot(ItemSlotWrapper slot)
		{
			// If the material slot has no item, return
			if (slot.Item.IsAir)
				return;

			// Return the item to the player when the UI is closed/changed
			Main.LocalPlayer.QuickSpawnClonedItem(slot.Item, slot.Item.stack);
			slot.Item.TurnToAir();
		}

		private void OnClickPartText(UIMouseEvent evt)
		{
			// If a part text was clicked, update the info

			if (!(evt.Target is UITextData<BasePart> uiText))
				return;

			UpdateInfo(uiText.extraData);
		}

		private void OnInputChanged(Item item)
		{
			if (!TerrarianWeaponry.Instance.RegisteredMaterials.TryGetValue(item.type, out BaseMaterial material))
			{
				// If there's no registered item with the item's type, clear all info

				UpdateParts(null);
				UpdateInfo(null);
				
				return;
			}

			UpdateInfo(null);
			UpdateParts(material);
		}

		private void OnCreateClicked(UIMouseEvent evt, UIElement element)
		{
			// If there's no selected part, return
			if (_selectedPart == null)
				return;

			// If the output slot already has an item, return
			if (!_outputSlot.Item.IsAir)
				return;

			// If the item doesn't create a material, return
			if (!TerrarianWeaponry.Instance.RegisteredMaterials.TryGetValue(_inputSlot.Item.type, out BaseMaterial material))
				return;

			// If all of the valid materials of the selected part are not equal to the current material, return
			if (_selectedPart.ValidMaterials.All(t => t.material != material))
				return;

			// If the input slot can't be consumed, return
			if (!_inputSlot.Item.SafeConsume(_selectedPart.MaterialCost))
				return;

			// Get the item ID from the internal name
			var itemId = TerrarianWeaponry.Instance.ItemType($"{_selectedPart.PartName}: {material.MaterialName}");
			if (itemId == 0)
				return;

			// Create the final item and set the defaults to the item id
			var finalItem = new Item();
			finalItem.SetDefaults(itemId);

			// Set the output slot
			_outputSlot.Item = finalItem;

			// Play the reforge sound
			Main.PlaySound(SoundID.Item, Style: 37);

			// If the input slot is empty at the end, clear the info and tools
			if (_inputSlot.Item.IsAir || _inputSlot.Item.stack == 0)
			{
				UpdateInfo(null);
				UpdateParts(null);
			}
		}

		private void UpdateParts(BaseMaterial material)
		{
			foreach (UIElement toolsListItem in _partsList._items)
			{
				if (!(toolsListItem is UITextData<BasePart> textPart))
					continue;

				textPart.CanBeClicked = textPart.extraData.ValidMaterials.Any(t => t.material == material);
			}
		}

		private void UpdateInfo(BasePart part)
		{
			_selectedPart = part;

			if (part == null)
			{
				// If the part is null, use an empty texture
				var texture = new Texture2D(Main.instance.GraphicsDevice, 1, 1);
				_partInfoImage.SetImage(texture);
				// Calculate the scale so that it fits inside the top 20 pixels
				_partInfoImage.ImageScale = 20f / texture.Height;

				// Clear the name and description
				_partInfoName.SetText("");
				_partInfoDescription.Clear();

				return; 
			}

			// Get the texture of the first valid material
			TextureInfo textureInfo = part.ValidMaterials.First().textureInfo;
			Texture2D realTexture = TerrarianWeaponry.Instance.GetTexture(textureInfo.Texture);
			_partInfoImage.SetImage(realTexture);
			// Calculate the scale so that it fits inside the top 20 pixels
			_partInfoImage.ImageScale = 20f / realTexture.Height;
			_partInfoImage.Left = new StyleDimension(0, 0);
			_partInfoImage.Top = realTexture.Height > 20 
				? new StyleDimension(0, 0) 
				: new StyleDimension(5, 0);

			// Set the tool name
			_partInfoName.SetText(part.PartName);

			// Set the description
			_partInfoDescription.Clear();

			// Add a line that shows the cost of the part
			_partInfoDescription.Add(new UIText($"Cost: {part.MaterialCost}"));

			var wrappedText = MiscUtils.WrapText(part.Description);
			foreach (string text in wrappedText)
				_partInfoDescription.Add(new UIText(text));
		}
	}
}
