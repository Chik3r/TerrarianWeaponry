using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader.UI;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;

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
				new Tab("Part Assembler", new PartAssemblerState()),
				new Tab("Tool Assembler", this));

			_tabPanel.Left.Set(DraggableUIPanel.lastPos.X, 0);
			_tabPanel.Top.Set(DraggableUIPanel.lastPos.Y, 0);
			_tabPanel.OnCloseBtnClicked += () => TerrarianWeaponry.Instance.UserInterface.SetState(null);

			_materialSlot = new ItemSlotWrapper
			{
				VAlign = 0.5f,
				Left = new StyleDimension(90, 0)
			};
			_materialSlot.OnItemChanged += OnItemChanged;
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
				Height = new StyleDimension(0, 1),
				Left = new StyleDimension(5, 0),
				Top = new StyleDimension(5, 0)
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

		private void OnItemChanged(Item item)
		{
			_infoList.Clear();

			// Check if there's a material with the item type as a valid item
			if (!TerrarianWeaponry.Instance.RegisteredMaterials.TryGetValue(item.type, out BaseMaterial material))
				return;

			// Create a new empty item info
			ItemInfo itemInfo = new ItemInfo();

			if (material.Modifier != null)
				_infoList.Add(new UIText($"Modifier: {material.Modifier}"));

			// Modify the stats of the item info, with a default modifier
			material.ModifyStats(ref itemInfo, 1f);

			// Get all modified fields and make a new UIText for them
			foreach (string modifiedField in itemInfo.GetModifiedFields()) 
				_infoList.Add(new UIText(modifiedField));
		}
	}
}
