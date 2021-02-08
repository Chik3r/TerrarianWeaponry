using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TerrarianWeaponry.DataLoading.Data;
using TerrarianWeaponry.DataLoading.Data.Parts;

namespace TerrarianWeaponry.DataLoading
{
	public class DataLoader
	{
		public static void LoadData()
		{
			// BaseTool
			//  -> Part Foo
			//		-> Material Alpha
			//		-> Material Gamma
			//  -> Part Bar
			//		-> Material Beta
			//		-> Material Gamma

			foreach (BaseTool baseTool in GetTools())
			{
				//float maxModifier = LoadParts(baseTool)
				//	.Max(p => 
				//		LoadMaterials(p).Max(m => m.Modifier)) ?? 1f;
				var combos = GetMaterialCombinations(baseTool);
				foreach (var materialCombo in combos)
				{
					var info = new ItemInfo();
					float maxModifier = materialCombo.Max(m => m.Modifier) ?? 1f;
					string toolName = $"{baseTool.ToolName}: {string.Join("-", materialCombo.Select(x => x.MaterialName))}";

					foreach (BaseMaterial material in materialCombo)
						material.ModifyStats(ref info, maxModifier);

					var textures = materialCombo.Select(m => TerrarianWeaponry.Instance.GetTexture(m.Texture)).ToArray();
					Point originPoint = materialCombo.First().OriginPoint;
					Texture2D finalTexture = textures.First();

					for (int i = 1; i < textures.Length; i++)
					{
						finalTexture = Utilities.MixTexture2D(finalTexture, originPoint, textures[i], materialCombo[i].OriginPoint);
					}
					
					RegisterItem(baseTool, toolName, info, finalTexture);
				}
			}
		}

		private static void RegisterItem(BaseTool tool, string itemName, ItemInfo info, Texture2D texture)
		{
			var toolType = tool.GetType();
			var finalItem = (BaseTool) Activator.CreateInstance(toolType, info, texture);

			TerrarianWeaponry.Instance.AddItem(itemName, finalItem);
		}

		private static IEnumerable<BaseTool> GetTools()
		{
			var baseType = typeof(BaseTool);
			var childTypes = TerrarianWeaponry.Instance.Code.GetTypes()
				.Where(t => baseType.IsAssignableFrom(t) && !t.IsAbstract);

			foreach (Type childType in childTypes)
			{
				yield return (BaseTool) Activator.CreateInstance(childType, new object[] {null, null});
			}
		}

		private static List<List<BaseMaterial>> GetMaterialCombinations(BaseTool tool)
		{
			var parts = LoadParts(tool);
			var materials = from part in parts
				select LoadMaterials(part);

			return InternalMaterialCombinations(materials);
		}

		private static List<List<BaseMaterial>> InternalMaterialCombinations(IEnumerable<IEnumerable<BaseMaterial>> remainingMaterials)
		{
			// Convert remainingMaterials to an array so that it isn't enumerated multiple times
			var remainingMaterialsEnumerated = remainingMaterials.Select(x => x.ToArray()).ToArray();

			// If there's only one material array left, make a list of lists out of it
			if (remainingMaterialsEnumerated.Count() == 1)
				return remainingMaterialsEnumerated.First().Select(r => new List<BaseMaterial> { r }).ToList();

			// Get the first material array, and then get combos for other material arrays
			var current = remainingMaterialsEnumerated.First();
			var combos = InternalMaterialCombinations(remainingMaterialsEnumerated.Where(x => x != current));

			// Return combos
			return (from materialPart in current
				from combo in combos
				select combo.Concat(new[] { materialPart }).ToList()).ToList();
		}

		private static IEnumerable<BasePart> LoadParts(BaseTool baseTool)
		{
			var baseType = typeof(BasePart);

			foreach (Type toolPartType in baseTool.ToolParts)
			{
				if (!baseType.IsAssignableFrom(toolPartType))
					throw new ArgumentException("All types inside ToolParts should extend BasePart");

				yield return (BasePart)Activator.CreateInstance(toolPartType);
			}
		}

		private static IEnumerable<BaseMaterial> LoadMaterials(BasePart basePart)
		{
			var baseType = typeof(BaseMaterial);

			foreach (Type toolPartType in basePart.ValidMaterials)
			{
				if (!baseType.IsAssignableFrom(toolPartType))
					throw new ArgumentException("All types inside ValidMaterials should extend BaseMaterial");

				yield return (BaseMaterial)Activator.CreateInstance(toolPartType);
			}
		}
	}
}
