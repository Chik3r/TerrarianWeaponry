using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrarianWeaponry.Items
{
	public class BaseItem : ModItem
	{
		public override string Texture => "TerrarianWeaponry/MissingItem";
		// If this is not set to true, _info will be set to null and this solution won't work
		public override bool CloneNewInstances => true;
		protected ItemInfo _info;

		public BaseItem() { } // Needed for Autoload to work

		public BaseItem(ItemInfo itemInfo)
		{
			_info = itemInfo;
		}

		public override void SetDefaults()
		{
			SetDefaultsFromInfo(); // Load the item values from _info
		}

		public override bool Autoload(ref string name)
		{
			// Create a new ItemInfo with the following values
			ItemInfo info = new ItemInfo
			{
				width = 40,
				height = 40,
				melee = true,
				useStyle = ItemUseStyleID.SwingThrow,
				useTime = 20,
				useAnimation = 20,
				UseSound = SoundID.Item1,
				autoReuse = true,
				damage = 1,
				knockBack = 0.5f
			};
			// Register a new item, the "name" parameter must be unique or it will lead to an exception
			mod.AddItem("BaseItem", new BaseItem(info)); 

			info = new ItemInfo
			{
				width = 40,
				height = 40,
				melee = true,
				useStyle = ItemUseStyleID.SwingThrow,
				useTime = 20,
				useAnimation = 20,
				UseSound = SoundID.Item1,
				autoReuse = true,
				damage = 200,
				knockBack = 2
			};

			mod.AddItem("StrongSword", new BaseItem(info));
			return false; // Return false to not create the default item since we already created our items 
		}

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
