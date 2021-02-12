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

		internal readonly Dictionary<string, BaseTool> RegisteredTools = new Dictionary<string, BaseTool>();
		internal readonly Dictionary<int, BaseMaterial> RegisteredMaterials = new Dictionary<int, BaseMaterial>();

		//private UIState _toolForgeUI;
		internal UserInterface UserInterface;

		public override void Load()
		{
			DataLoader.LoadData();

			var _toolForgeUI = new MaterialInfoState();
			_toolForgeUI.Activate();
			UserInterface = new UserInterface();
			UserInterface.SetState(_toolForgeUI);
		}

		public override void UpdateUI(GameTime gameTime)
		{
			UserInterface?.Update(gameTime);
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
					UserInterface.Draw(Main.spriteBatch, null);
					return true;
				},
				InterfaceScaleType.UI));
		}
	}
}