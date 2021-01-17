using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrarianWeaponry.JsonData.New
{

	public class Rootobject
	{
		public Tool[] Tools { get; set; }
		public Part[] Parts { get; set; }
		public Material[] Materials { get; set; }
	}

	public class Tool
	{
		public string ToolName { get; set; }
		public string[] Parts { get; set; }
	}

	public class Part
	{
		public string PartName { get; set; }
		public string[] ValidMaterials { get; set; }
	}

	public class Material
	{
		public string MaterialName { get; set; }
		public int Damage { get; set; }
		public float Knockback { get; set; }
		public int Mana { get; set; }
		public int Pick { get; set; }
		public int Axe { get; set; }
		public int Hammer { get; set; }
		public int TileBoost { get; set; }
		public int UseAnimation { get; set; }
		public int UseTime { get; set; }
		public int Scale { get; set; }
		public int UseStyle { get; set; }
		public bool UseTurn { get; set; }
		public bool AutoReuse { get; set; }
		public int Rare { get; set; }
		public float Modifier { get; set; }
		public string TexturePath { get; set; }
	}

}
