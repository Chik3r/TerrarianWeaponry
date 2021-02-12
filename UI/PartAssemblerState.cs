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

			var otherPanel = new UIPanel
			{
				Left = new StyleDimension(_tabPanel.Width.Pixels - 150 - 20, 0),
				Top = new StyleDimension(40, 0),
				Width = new StyleDimension(150, 0),
				Height = new StyleDimension(190, 0)
			};
			otherPanel.SetPadding(0);
			_tabPanel.Append(otherPanel);

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

			Main.NewText($"Wow: {uiText.basePart.PartName}");
		}
	}
}
