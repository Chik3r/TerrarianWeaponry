using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	internal class MaterialInfoState : UIState
	{
		public override void OnInitialize()
		{
			TabPanel panel = new TabPanel(500, 250,
				new Tab("Material Info", this),
				new Tab("Part Assembler", this),
				new Tab("Tool Assembler", this));

			panel.Left.Set(400, 0);
			panel.Top.Set(400, 0);
			panel.OnCloseBtnClicked += () => TerrarianWeaponry.Instance.UserInterface.SetState(null);

			Append(panel);
		}
	}
}
