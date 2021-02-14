using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;

namespace TerrarianWeaponry.UI
{
	internal class PartAssemblerState : UIState
	{
		private TabPanel _tabPanel;
		private UIPanel _toolsPanel;
		private UIList _toolsList;
		private ItemSlotWrapper _materialSlot;
		
		private UIPanel _toolInfoPanel;
		private UIImage _toolInfoImage;
		private UIText _toolInfoName;
		private UIList _toolInfoDescription;

		public override void OnInitialize()
		{
			// Create a tab panel
			_tabPanel = new TabPanel(500, 250,
				new Tab("Material Info", new MaterialInfoState()),
				new Tab("Part Assembler", this),
				new Tab("Tool Assembler", this));

			_tabPanel.Left.Set(DraggableUIPanel.lastPos.X, 0);
			_tabPanel.Top.Set(DraggableUIPanel.lastPos.Y, 0);
			_tabPanel.OnCloseBtnClicked += () => TerrarianWeaponry.Instance.UserInterface.SetState(null);

			#region Create panel containing tools

			_toolsPanel = new UIPanel
			{
				Left = new StyleDimension(20, 0),
				Top = new StyleDimension(40, 0),
				Width = new StyleDimension(150, 0),
				Height = new StyleDimension(190, 0)
			};
			_toolsPanel.SetPadding(0);
			_tabPanel.Append(_toolsPanel);

			// Create a list
			_toolsList = new UIList
			{
				Width = new StyleDimension(125, 0),
				Height = new StyleDimension(0, 1),
				Left = new StyleDimension(25, 0),
				Top = new StyleDimension(5, 0)
			};
			_toolsPanel.Append(_toolsList);

			// And add a scrollbar
			UIScrollbar toolScrollbar = new UIScrollbar
			{
				Height = new StyleDimension(180, 0),
				Top = new StyleDimension(5, 0),
				Width = new StyleDimension(20, 0),
				Left = new StyleDimension(0, 0)
			}.WithView(50, 250);
			_toolsPanel.Append(toolScrollbar);
			_toolsList.SetScrollbar(toolScrollbar);

			AddToolsToList();

			#endregion

			_materialSlot = new ItemSlotWrapper
			{
				Top = new StyleDimension(90, 0),
				HAlign = 0.5f
			};
			//_materialSlot.OnItemChanged += OnItemChanged;
			_tabPanel.Append(_materialSlot);

			#region Create panel for part info

			_toolInfoPanel = new UIPanel
			{
				Left = new StyleDimension(_tabPanel.Width.Pixels - 150 - 20, 0),
				Top = new StyleDimension(40, 0),
				Width = new StyleDimension(150, 0),
				Height = new StyleDimension(190, 0)
			};
			_toolInfoPanel.SetPadding(0);
			_tabPanel.Append(_toolInfoPanel);

			// Create an image for the info with a default empty image
			var texture = new Texture2D(Main.instance.GraphicsDevice, 1, 1);
			_toolInfoImage = new UIImage(texture)
			{
				HAlign = .5f, 
				ImageScale = 20f / (texture.Width > texture.Height ? texture.Width : texture.Height),
			};
			_toolInfoImage.SetImage(texture);
			_toolInfoImage.Width = new StyleDimension(20, 0);
			_toolInfoImage.Height = new StyleDimension(20, 0);
			_toolInfoImage.Left = new StyleDimension(-10, 0);
			_toolInfoPanel.Append(_toolInfoImage);

			// Add a UIText for the name of the part
			_toolInfoName = new UIText("")
			{
				Top = new StyleDimension(35, 0),
				HAlign = 0.5f,
				Left = new StyleDimension(-5, 0)
			};
			_toolInfoPanel.Append(_toolInfoName);

			// Add a UIList for the description of the part, each line is a new element in the list
			_toolInfoDescription = new UnsortedList
			{
				Top = new StyleDimension(60, 0),
				Left = new StyleDimension(8, 0),
				Width = new StyleDimension(_toolInfoPanel.Width.Pixels - 20, 0),
				Height = new StyleDimension(125, 0)
			};
			_toolInfoPanel.Append(_toolInfoDescription);

			UIScrollbar descriptionScrollbar = new UIScrollbar
			{
				Height = new StyleDimension(_toolInfoPanel.Height.Pixels - 10, 0),
				Top = new StyleDimension(5, 0),
				Width = new StyleDimension(20, 0),
				Left = new StyleDimension(_toolInfoPanel.Width.Pixels - 20, 0)
				//HAlign = 1f
			}.WithView(20, 130);
			_toolInfoPanel.Append(descriptionScrollbar);
			_toolInfoDescription.SetScrollbar(descriptionScrollbar);
			
			#endregion

			Append(_tabPanel);
		}

		private void AddToolsToList()
		{
			foreach (BasePart basePart in Utilities.GetTypesExtendingT<BasePart>())
			{
				_toolsList.Add(new UITextBasePart(basePart.PartName)
				{
					basePart = basePart,
					OnClicked = OnClickPartText
				});
			}
		}

		public override void OnDeactivate()
		{
			// If the material slot has no item, return
			if (_materialSlot.Item.IsAir)
				return;

			// Return the item to the player when the UI is closed/changed
			Main.LocalPlayer.QuickSpawnClonedItem(_materialSlot.Item, _materialSlot.Item.stack);
			_materialSlot.Item.TurnToAir();
		}

		private void OnClickPartText(UIMouseEvent evt)
		{
			if (!(evt.Target is UITextBasePart uiText))
				return;

			UpdateInfo(uiText.basePart);
		}

		private IEnumerable<string> WrapText(string inputString)
		{
			int characterLimit = 14;

			string[] splitText = inputString.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
			StringBuilder sb = new StringBuilder();

			string line = "";
			foreach (string word in splitText)
			{
				if ((line + word).Length > characterLimit)
				{
					sb.AppendLine(line);
					line = "";
				}

				line += $"{word} ";
			}

			if (line.Length > 0)
				sb.AppendLine(line);

			return sb.ToString().Split(new []{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);
		}

		private void UpdateInfo(BasePart part)
		{
			if (part == null)
			{
				var texture = new Texture2D(Main.instance.GraphicsDevice, 1, 1);
				_toolInfoImage.SetImage(texture);
				_toolInfoImage.ImageScale = 20f / (texture.Width > texture.Height ? texture.Width : texture.Height);

				_toolInfoName.SetText("");

				_toolInfoDescription.Clear();
				return; 
			}

			TextureInfo textureInfo = part.ValidMaterials.First().textureInfo;
			Texture2D realTexture = TerrarianWeaponry.Instance.GetTexture(textureInfo.Texture);
			_toolInfoImage.SetImage(realTexture);
			_toolInfoImage.ImageScale = 20f / (realTexture.Width > realTexture.Height ? realTexture.Width : realTexture.Height);
			_toolInfoImage.Left = new StyleDimension(0, 0);
			_toolInfoImage.Top = realTexture.Height > 20 
				? new StyleDimension(0, 0) 
				: new StyleDimension(5, 0);

			_toolInfoName.SetText(part.PartName);

			_toolInfoDescription.Clear();
			var wrappedText = WrapText(part.Description);
			foreach (string text in wrappedText)
				_toolInfoDescription.Add(new UIText(text));
		}
	}
}
