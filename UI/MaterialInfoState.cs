using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	internal class MaterialInfoState : UIState
	{
		private TabPanel _tabPanel;
		private ItemSlotWrapper _materialSlot;
		private UIPanel _infoPanel;
		private UIList _infoList;

		public override void OnInitialize()
		{
			_tabPanel = new TabPanel(500, 250,
				new Tab("Material Info", this),
				new Tab("Part Assembler", this),
				new Tab("Tool Assembler", this));

			_tabPanel.Left.Set(400, 0);
			_tabPanel.Top.Set(400, 0);
			_tabPanel.OnCloseBtnClicked += () => TerrarianWeaponry.Instance.UserInterface.SetState(null);

			_materialSlot = new ItemSlotWrapper
			{
				VAlign = 0.5f,
				Left = new StyleDimension(90, 0)
			};
			_tabPanel.Append(_materialSlot);

			#region Create Material Info Panel

			_infoPanel = new UIPanel
			{
				Left = new StyleDimension(230, 0),
				Width = new StyleDimension(255, 0),
				Top = new StyleDimension(40, 0),
				Height = new StyleDimension(195, 0)
			};
			_infoPanel.SetPadding(0);
			_tabPanel.Append(_infoPanel);

			// Create a list
			_infoList = new UIList
			{
				Width = new StyleDimension(235, 0),
				Height = new StyleDimension(0, 1)
			};
			_infoPanel.Append(_infoList);

			// And add a scrollbar
			UIScrollbar infoScrollbar = new UIScrollbar
			{
				Height = new StyleDimension(185, 0),
				Top = new StyleDimension(5, 0),
				Width = new StyleDimension(20, 0),
				Left = new StyleDimension(235, 0)
			}.WithView(50, 250);
			_infoPanel.Append(infoScrollbar);
			_infoList.SetScrollbar(infoScrollbar);

			#endregion

			Append(_tabPanel);
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
	}
}
