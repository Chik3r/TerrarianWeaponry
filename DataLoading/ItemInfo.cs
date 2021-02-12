using System.Collections.Generic;
using System.Reflection;
using Terraria.Audio;

#pragma warning disable 649
// ReSharper disable InconsistentNaming

namespace TerrarianWeaponry.DataLoading
{
	public struct ItemInfo
	{
		private FieldInfo[] fields;

		public bool? mech;
		public int? fishingPole;
		public int? bait;
		public bool? expert;
		public int? holdStyle;
		public int? useStyle;
		public bool? channel;
		public bool? accessory;
		public int? useAnimation;
		public int? useTime;
		public int? maxStack;
		public int? pick;
		public int? axe;
		public int? hammer;
		public int? tileBoost;
		public int? damage;
		public float? knockBack;
		public int? healLife;
		public int? healMana;
		public bool? potion;
		public bool? consumable;
		public bool? autoReuse;
		public bool? useTurn;
		public float? scale;
		public LegacySoundStyle UseSound; // TODO: UseSound stuff
		public int? defense;
		public int? rare;
		public int? shoot;
		public float? shootSpeed;
		public int? ammo;
		public bool? notAmmo;
		public int? useAmmo;
		public int? lifeRegen;
		public int? manaIncrease;
		public int? mana;
		public bool? noMelee;
		public int? value;
		public bool? material;
		public int? buffType;
		public int? buffTime;
		public int? mountType;
		public int? crit;
		public bool? melee;
		public bool? magic;
		public bool? ranged;
		public bool? thrown;
		public bool? summon;
		public bool? sentry;
		public int? reuseDelay;
		public int? width;
		public int? height;

		private IEnumerable<FieldInfo> GetModifiedFieldInfo()
		{
			if (fields == null)
				fields = typeof(ItemInfo).GetFields();

			foreach (FieldInfo field in fields)
			{
				if (field.GetValue(this) != null)
					yield return field;
			}
		}

		public IEnumerable<string> GetModifiedFields()
		{
			foreach (FieldInfo modifiedField in GetModifiedFieldInfo())
			{
				yield return $"{modifiedField.Name}: {modifiedField.GetValue(this)}";
			}
		}
	}
}
