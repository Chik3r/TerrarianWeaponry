using System.Collections.Generic;
using Newtonsoft.Json;

namespace TerrarianWeaponry.JsonData
{
	public class MainData
	{
		public List<ToolData> Tools { get; set; }

		public List<PartData> Parts { get; set; }

		public List<MaterialData> Materials { get; set; }

		public static string ToJson(MainData data) => JsonConvert.SerializeObject(data);

		public static MainData FromJson(string data) => JsonConvert.DeserializeObject<MainData>(data);
	}
}
