namespace TerrarianWeaponry.JsonData
{
	public class MaterialData
	{
		[FieldNameReference("")]
		[ModifierType(ModifierTypes.None)]
		public string MaterialName { get; set; }

		[FieldNameReference("")]
		[ModifierType(ModifierTypes.Modifier)]
		public float Modifier { get; set; }

		[FieldNameReference("")]
		[ModifierType(ModifierTypes.None)]
		public string TexturePath { get; set; }

		[FieldNameReference("")]
		[ModifierType(ModifierTypes.None)]
		public int OriginX { get; set; }

		[FieldNameReference("")]
		[ModifierType(ModifierTypes.None)]
		public int OriginY { get; set; }

		[FieldNameReference("damage")]
		[ModifierType(ModifierTypes.MultiplyInt)]
		public int Damage { get; set; }

		[FieldNameReference("knockBack")]
		[ModifierType(ModifierTypes.MultiplyFloat)]
		public float Knockback { get; set; }

		[FieldNameReference("mana")]
		[ModifierType(ModifierTypes.DivideInt)]
		public int Mana { get; set; }

		[FieldNameReference("pick")]
		[ModifierType(ModifierTypes.MultiplyInt)]
		public int Pick { get; set; }

		[FieldNameReference("axe")]
		[ModifierType(ModifierTypes.MultiplyInt)]
		public int Axe { get; set; }

		[FieldNameReference("hammer")]
		[ModifierType(ModifierTypes.MultiplyInt)]
		public int Hammer { get; set; }

		[FieldNameReference("tileBoost")]
		[ModifierType(ModifierTypes.MultiplyInt)]
		public int TileBoost { get; set; }

		[FieldNameReference("useAnimation")]
		[ModifierType(ModifierTypes.DivideInt)]
		public int UseAnimation { get; set; }

		[FieldNameReference("useTime")]
		[ModifierType(ModifierTypes.DivideInt)]
		public int UseTime { get; set; }

		[FieldNameReference("scale")]
		[ModifierType(ModifierTypes.None)]
		public float Scale { get; set; }

		[FieldNameReference("useStyle")]
		[ModifierType(ModifierTypes.None)]
		public int UseStyle { get; set; }

		[FieldNameReference("useTurn")]
		[ModifierType(ModifierTypes.None)]
		public bool UseTurn { get; set; }

		[FieldNameReference("autoReuse")]
		[ModifierType(ModifierTypes.None)]
		public bool AutoReuse { get; set; }

		[FieldNameReference("rare")]
		[ModifierType(ModifierTypes.None)]
		public int Rare { get; set; }
	}
}