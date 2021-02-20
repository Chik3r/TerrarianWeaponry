using System;
using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	class UITextData<T> : UIText
	{
		internal T extraData;
		internal Action<UIMouseEvent> OnClicked;
		private bool _canBeClicked = true;

		public bool CanBeClicked
		{
			get => _canBeClicked;
			set
			{
				_canBeClicked = value;
				TextColor = value 
					? Color.White 
					: Color.Gray;
			}
		}

		public UITextData(string text, float textScale = 1, bool large = false) : base(text, textScale, large) { }

		public override void Click(UIMouseEvent evt)
		{
			base.Click(evt);
			if (CanBeClicked)
				OnClicked?.Invoke(evt);
		}
	}
}
