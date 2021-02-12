using Terraria.ModLoader;
using TerrarianWeaponry.UI;

namespace TerrarianWeaponry
{
#if DEBUG
	class DebugUICommand : ModCommand
	{
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			TerrarianWeaponry.Instance.UserInterface.SetState(null);
			var _toolForgeUI = new MaterialInfoState();
			_toolForgeUI.Activate();
			TerrarianWeaponry.Instance.UserInterface.SetState(_toolForgeUI);
		}

		public override string Command => "ui";
		public override CommandType Type => CommandType.Chat;
	}
#endif
}
