using System.Reflection;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace TerrarianWeaponry.UI
{
	internal class UnsortedList : UIList
	{
		private FieldInfo _innerListField = typeof(UIList).GetField("_innerList", BindingFlags.NonPublic | BindingFlags.Instance);

		public override void Add(UIElement item)
		{
			UIElement _innerList = (UIElement) _innerListField.GetValue(this);
			_items.Add(item);
			_innerList.Append(item);
			UpdateOrder();
			_innerList.Recalculate();
			_innerListField.SetValue(this, _innerList);
		}
	}
}
