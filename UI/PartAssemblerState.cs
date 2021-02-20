using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader.UI;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;
using TerrarianWeaponry.Utilities;

namespace TerrarianWeaponry.UI
{
	internal class PartAssemblerState : UIState
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
			// Create a tab panel
			_tabPanel = new TabPanel(500, 250,
				new Tab("Material Info", new MaterialInfoState()),
				new Tab("Part Assembler", this),
				new Tab("Tool Assembler", new ToolAssemblerState()));

			_tabPanel.Left.Set(DraggableUIPanel.lastPos.X, 0);
			_tabPanel.Top.Set(DraggableUIPanel.lastPos.Y, 0);
			_tabPanel.OnCloseBtnClicked += () => TerrarianWeaponry.Instance.UpdateState(null);

			#region Create panel containing tools

			_partsPanel = new UIPanel
			{
				Left = new StyleDimension(20, 0),
				Top = new StyleDimension(40, 0),
				Width = new StyleDimension(150, 0),
				Height = new StyleDimension(190, 0)
			};
			_partsPanel.SetPadding(0);
			_tabPanel.Append(_partsPanel);

			// Create a list
			_partsList = new UIList
			{
				Width = new StyleDimension(125, 0),
				Height = new StyleDimension(0, 1),
				Left = new StyleDimension(25, 0),
				Top = new StyleDimension(5, 0)
			};
			_partsPanel.Append(_partsList);

			// And add a scrollbar
			UIScrollbar toolScrollbar = new UIScrollbar
			{
				Height = new StyleDimension(180, 0),
				Top = new StyleDimension(5, 0),
				Width = new StyleDimension(20, 0),
				Left = new StyleDimension(0, 0)
			}.WithView(50, 250);
			_partsPanel.Append(toolScrollbar);
			_partsList.SetScrollbar(toolScrollbar);

			AddPartsToList();

			UpdateParts(null);

			#endregion

			#region Create part assembler

			// Create the input slot and add a callback when the item changes
			_inputSlot = new ItemSlotWrapper
			{
				Top = new StyleDimension(50, 0),
				HAlign = 0.5f
			};
			_inputSlot.OnItemChanged += OnInputChanged;
			_tabPanel.Append(_inputSlot);

			// Create the "Create" button and add a callback when it is clicked
			_createBtn = new UITextPanel<string>("Create!")
			{
				HAlign = 0.5f,
				Top = new StyleDimension(5, 0),
				VAlign = 0.5f
			};
			_createBtn.OnClick += OnCreateClicked;
			_tabPanel.Append(_createBtn);

			// Create the output slot, and make it not possible to insert into it
			_outputSlot = new ItemSlotWrapper(canInsert: false)
			{
				Top = new StyleDimension(160, 0),
				HAlign = 0.5f
			};
			_tabPanel.Append(_outputSlot);

			#endregion

			#region Create panel for part info

			_partInfoPanel = new UIPanel
			{
				Left = new StyleDimension(_tabPanel.Width.Pixels - 150 - 20, 0),
				Top = new StyleDimension(40, 0),
				Width = new StyleDimension(150, 0),
				Height = new StyleDimension(190, 0)
			};
			_partInfoPanel.SetPadding(0);
			_tabPanel.Append(_partInfoPanel);

			// Create an image for the info with a default empty image
			var texture = new Texture2D(Main.instance.GraphicsDevice, 1, 1);
			_partInfoImage = new UIImage(texture)
			{
				HAlign = .5f, 
				ImageScale = 20f / (texture.Width > texture.Height ? texture.Width : texture.Height),
			};
			_partInfoImage.SetImage(texture);
			_partInfoImage.Width = new StyleDimension(20, 0);
			_partInfoImage.Height = new StyleDimension(20, 0);
			_partInfoImage.Left = new StyleDimension(-10, 0);
			_partInfoPanel.Append(_partInfoImage);

			// Add a UIText for the name of the part
			_partInfoName = new UIText("")
			{
				Top = new StyleDimension(35, 0),
				HAlign = 0.5f,
				Left = new StyleDimension(-5, 0)
			};
			_partInfoPanel.Append(_partInfoName);

			// Add a UIList for the description of the part, each line is a new element in the list
			_partInfoDescription = new UnsortedList
			{
				Top = new StyleDimension(60, 0),
				Left = new StyleDimension(8, 0),
				Width = new StyleDimension(_partInfoPanel.Width.Pixels - 20, 0),
				Height = new StyleDimension(125, 0)
			};
			_partInfoPanel.Append(_partInfoDescription);

			UIScrollbar descriptionScrollbar = new UIScrollbar
			{
				Height = new StyleDimension(_partInfoPanel.Height.Pixels - 10, 0),
				Top = new StyleDimension(5, 0),
				Width = new StyleDimension(20, 0),
				Left = new StyleDimension(_partInfoPanel.Width.Pixels - 20, 0)
				//HAlign = 1f
			}.WithView(20, 130);
			_partInfoPanel.Append(descriptionScrollbar);
			_partInfoDescription.SetScrollbar(descriptionScrollbar);
			
			#endregion

			Append(_tabPanel);
		}

		private void AddPartsToList()
		{
			foreach (BasePart basePart in MiscUtils.GetTypesExtendingT<BasePart>())
			{
				_partsList.Add(new UITextBasePart(basePart.PartName)
				{
					basePart = basePart,
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

			if (!(evt.Target is UITextBasePart uiText))
				return;

			UpdateInfo(uiText.basePart);
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
				if (!(toolsListItem is UITextBasePart textPart))
					continue;

				textPart.CanBeClicked = textPart.basePart.ValidMaterials.Any(t => t.material == material);
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
