using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	internal class Tab : UITextPanel<string>
	{
		private readonly UIState _targetState;

		// Add a space at the start and end to make it not as cramped
		// Also add another extra space at the end so that tabs don't overlap their text
		public Tab(string text, UIState targetState) : base($" {text.Trim()}  ")
		{
			_targetState = targetState;
		}

		public override void OnInitialize()
		{
			SetPadding(7);
			BackgroundColor.A = 255;
		}

		public override void Click(UIMouseEvent evt)
		{
			// When the tab is clicked, change the UIState of the UserInterface, and also change the LastTab
			TabPanel.LastTab = _targetState;

			TerrarianWeaponry.Instance.UserInterface.SetState(_targetState);
			Main.PlaySound(SoundID.MenuTick);
		}

		public override void Update(GameTime gameTime)
		{
			// If the current state is the same as the target state, change the background color to a darker blue
			if (TerrarianWeaponry.Instance.UserInterface.CurrentState == _targetState)
				BackgroundColor = new Color(73, 94, 171);
		}
	}
}
