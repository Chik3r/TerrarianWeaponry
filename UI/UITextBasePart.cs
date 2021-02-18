using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;

namespace TerrarianWeaponry.UI
{
	class UITextBasePart : UIText
	{
		internal BasePart basePart;
		internal Action<UIMouseEvent> OnClicked;
		private bool _canBeClicked = true;

		public bool CanBeClicked
		{
			get => _canBeClicked;
			set
			{
				_canBeClicked = value;
				if (value)
					TextColor = Color.White;
				else
					TextColor = Color.Gray;
			}
		}

		public UITextBasePart(string text, float textScale = 1, bool large = false) : base(text, textScale, large) { }

		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
			if (CanBeClicked)
				OnClicked?.Invoke(evt);
		}
	}
}
