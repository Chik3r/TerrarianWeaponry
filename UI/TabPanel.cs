using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	internal class TabPanel : DraggableUIPanel
	{
		private readonly Tab[] _tabs;
		internal static UIState LastTab;

		public TabPanel(float width, float height, params Tab[] tabs) : base(width, height)
		{
			Width.Pixels = width;
			Height.Pixels = height;
			_tabs = tabs;
		}

		public override void OnInitialize()
		{
			// Set each tab to be next to the previous tab
			for (int i = 1; i < _tabs.Length; i++)
			{
				if (_tabs[i - 1] != null)
					_tabs[i].Left.Set((_tabs[i-1].MinWidth.Pixels - 24) + _tabs[i-1].Left.Pixels, 0f);
			}

			// Append the tabs to the header
			foreach (Tab tab in _tabs)
				header.Append(tab);
		}
	}
}
