using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;
using TerrarianWeaponry.UI;

namespace TerrarianWeaponry
{
	public class TerrarianWeaponry : Mod
	{
		public static TerrarianWeaponry Instance => ModContent.GetInstance<TerrarianWeaponry>();

		internal Dictionary<string, BaseTool> RegisteredTools = new Dictionary<string, BaseTool>();

		private ToolForgeUI _toolForgeUI;
		private UserInterface _toolForgeInterface;

		public override void Load()
		{
			DataLoader.LoadData();

			_toolForgeUI = new ToolForgeUI();
			_toolForgeUI.Activate();
			_toolForgeInterface = new UserInterface();
			_toolForgeInterface.SetState(_toolForgeUI);
		}

		public override void UpdateUI(GameTime gameTime)
		{
			_toolForgeUI?.Update(gameTime);
		}

		public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
		{
			int mouseLayerIndex = layers.FindIndex(layer => layer.Name == "Vanilla: Mouse Text");
			if (mouseLayerIndex == 1)
				return;

			layers.Insert(mouseLayerIndex, new LegacyGameInterfaceLayer(
				"TerrarianWeaponry: Tool Forge UI",
				delegate
				{
					_toolForgeUI.Draw(Main.spriteBatch);
					return true;
				},
				InterfaceScaleType.UI));
		}
	}
}