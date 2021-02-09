using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;

namespace TerrarianWeaponry.UI
{
	public class ToolForgeUI : UIState
	{
		private bool _populated = false;
		private UIList _materialList;

		public override void OnInitialize()
		{
			UIPanel panel = new UIPanel
			{
				Height = new StyleDimension(200, 0),
				Width = new StyleDimension(200, 0),
				HAlign = 0.5f,
				VAlign = 0.5f
			};

			_materialList = new UIList
			{
				Width = new StyleDimension(200, 1f),
				Height = new StyleDimension(200, 1f),
			};
			panel.Append(_materialList);

			var uiScrollbar = new UIScrollbar();
			uiScrollbar.SetView(100f, 1000f);
			uiScrollbar.Height.Set(0f, 1f);
			uiScrollbar.HAlign = 1f;
			panel.Append(uiScrollbar);
			_materialList.SetScrollbar(uiScrollbar);

			//panel.Append(new ItemSlotWrapper());

			//var b = new MaterialItemUI(a.Value.Materials.First())
			//{
			//	HAlign = 0.1f,
			//	VAlign = 0.1f,
			//	Width = new StyleDimension(200, 0),
			//	Height = new StyleDimension(80, 0)
			//};
			//panel.Append(b);

			Append(panel);
		}

		private void PopulateList()
		{
			_materialList.Clear();

			var a = TerrarianWeaponry.Instance.RegisteredTools.First();

			foreach (BaseMaterial material in a.Value.Materials)
			{
				var b = new MaterialItemUI(material)
				{
					//HAlign = 0.1f,
					//VAlign = 0.1f,
					Width = new StyleDimension(0, 1),
					Height = new StyleDimension(80, 0)
				};
				_materialList.Add(b);
			}
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			if (_populated)
				return;

			_populated = true;
			PopulateList();
		}

		protected override void DrawChildren(SpriteBatch spriteBatch)
		{
			base.DrawChildren(spriteBatch);
			//_materialList.Draw(spriteBatch);
		}
	}
}
