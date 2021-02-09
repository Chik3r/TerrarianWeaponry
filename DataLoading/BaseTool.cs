using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace TerrarianWeaponry.DataLoading
{
	public abstract class BaseTool : ModItem
	{
		private ItemInfo _info;
		private readonly Texture2D _texture;
		internal readonly List<BaseMaterial> Materials;

		/// <summary>
		/// Override this to set the list of parts of this tool.
		/// </summary>
		public abstract List<BasePart> ToolParts { get; }

		/// <summary>
		/// Override this to set the name of the tool, ex: a pickaxe would set this to "Pickaxe"
		/// </summary>
		public abstract string ToolName { get; }

		public BaseTool(ItemInfo info, Texture2D texture, List<BaseMaterial> materials)
		{
			_info = info;
			_texture = texture;
			Materials = materials;
		}

		#region ModItem methods

		public sealed override bool CloneNewInstances => true;

		public sealed override string Texture => "TerrarianWeaponry/MissingItem";

		public sealed override bool Autoload(ref string name) => false;

		public sealed override void SetStaticDefaults()
		{
			Main.itemTexture[item.type] = _texture;
			_info.width = _texture.Width;
			_info.height = _texture.Height;
		}

		public sealed override void SetDefaults()
		{
			// Set defaults from _info
			
			var infoFields = typeof(ItemInfo).GetFields(); // Gets all the fields in ItemInfo
			foreach (FieldInfo field in infoFields)
			{
				var infoFieldValue = field.GetValue(_info); // Gets the value of the field
				var itemField = typeof(Item).GetField(field.Name); // Gets the field with a matching name in Item

				// If the value of infoFieldValue is not null, set the item field to infoFieldValue
				if (infoFieldValue != null)
					itemField.SetValue(item, infoFieldValue);
			}

			SafeSetDefaults();
		}

		#endregion

		/// <summary>
		/// Override this method to set item defaults that all instances of this item will share, ex: a pickaxe would set useStyle to 1
		/// <br/> Called after the defaults are set from <see cref="_info"/>
		/// </summary>
		public virtual void SafeSetDefaults() { }
	}
}
