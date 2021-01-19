using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace TerrarianWeaponry.Items
{
	public class BaseItem : ModItem
	{
		public override string Texture => "TerrarianWeaponry/MissingItem";
		// If this is not set to true, _info will be set to null and this solution won't work
		public override bool CloneNewInstances => true;
		protected ItemInfo _info;
		protected readonly Texture2D _texture;

		public BaseItem() { } // Needed for Autoload to work

		public BaseItem(ItemInfo info, Texture2D texture = null)
		{
			_info = info;
			_texture = texture;
		}

		public override void SetStaticDefaults()
		{
			if (_texture != null)
			{
				Main.itemTexture[item.type] = _texture;
				_info.width = _texture.Width;
				_info.height = _texture.Height;
			}
		}

		public override void SetDefaults()
		{
			SetDefaultsFromInfo(); // Load the item values from _info
		}

		// Return false to not create the default item since we already created our items 
		public override bool Autoload(ref string name) => false;

		/// <summary>
		/// Sets the default values of an <see cref="Item"/> by getting the values from <see cref="_info"/>
		/// </summary>
		protected void SetDefaultsFromInfo()
		{
			var infoFields = typeof(ItemInfo).GetFields(); // Gets all the fields in ItemInfo
			foreach (FieldInfo field in infoFields)
			{
				var infoFieldValue = field.GetValue(_info); // Gets the value of the field
				var itemField = typeof(Item).GetField(field.Name); // Gets the field with a matching name in Item

				// If the value of infoFieldValue is not null, set the item field to infoFieldValue
				if (infoFieldValue != null) 
					itemField.SetValue(item, infoFieldValue); 
			}
		}
	}
}
