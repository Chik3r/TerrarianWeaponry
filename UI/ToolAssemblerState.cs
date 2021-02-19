using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	internal class ToolAssemblerState : UIState
	{
		private TabPanel _tabPanel;

		public override void OnInitialize()
		{
			_tabPanel = new TabPanel(500, 250,
				new Tab("Material Info", new MaterialInfoState()),
				new Tab("Part Assembler", new PartAssemblerState()),
				new Tab("Tool Assembler", this));

			_tabPanel.Left.Set(DraggableUIPanel.lastPos.X, 0);
			_tabPanel.Top.Set(DraggableUIPanel.lastPos.Y, 0);
			_tabPanel.OnCloseBtnClicked += () => TerrarianWeaponry.Instance.UpdateState(null);



			Append(_tabPanel);
		}
	}
}
