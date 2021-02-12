using System;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;
using TerrarianWeaponry.DataLoading;

namespace TerrarianWeaponry.UI
{
	class UITextBasePart : UIText
	{
		internal BasePart basePart;
		internal Action<UIMouseEvent> OnClicked;

		public UITextBasePart(string text, float textScale = 1, bool large = false) : base(text, textScale, large) { }

		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
			OnClicked?.Invoke(evt);
		}
	}
}
