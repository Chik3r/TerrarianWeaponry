using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	partial class PartAssemblerState
	{
		private void CreateMainPanel()
		{
			// Create a tab panel
			_tabPanel = new TabPanel(500, 250,
				new Tab("Material Info", new MaterialInfoState()),
				new Tab("Part Assembler", this),
				new Tab("Tool Assembler", new ToolAssemblerState()));

			_tabPanel.Left.Set(DraggableUIPanel.lastPos.X, 0);
			_tabPanel.Top.Set(DraggableUIPanel.lastPos.Y, 0);
			_tabPanel.OnCloseBtnClicked += () => TerrarianWeaponry.Instance.UpdateState(null);

			Append(_tabPanel);
		}

		private void CreatePartListPanel()
		{
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
				Width = new StyleDimension(_partsPanel.Width.Pixels - 25, 0),
				Height = new StyleDimension(0, 1),
				Left = new StyleDimension(25, 0),
				Top = new StyleDimension(5, 0)
			};
			_partsPanel.Append(_partsList);

			// And add a scrollbar
			UIScrollbar partScrollbar = new UIScrollbar
			{
				Height = new StyleDimension(_partsPanel.Height.Pixels - 10, 0),
				//Top = new StyleDimension(5, 0),
				VAlign = 0.5f,
				Width = new StyleDimension(20, 0),
				Left = new StyleDimension(0, 0)
			}.WithView(50, 250);
			_partsPanel.Append(partScrollbar);
			_partsList.SetScrollbar(partScrollbar);

			AddPartsToList();

			UpdateParts(null);
		}

		private void CreatePartAssembler()
		{
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
		}

		private void CreatePartInfoPanel()
		{
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
		}
	}
}
