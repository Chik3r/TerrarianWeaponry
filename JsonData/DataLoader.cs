using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using TerrarianWeaponry.Items;

namespace TerrarianWeaponry.JsonData
{
	internal class DataLoader
	{
		public MainData Data;
		//private Dictionary<Tuple<string, string>, Texture2D> te

		public DataLoader(MainData data)
		{
			Data = data;
		}

		public void LoadData()
		{
			Mod mod = ModContent.GetInstance<TerrarianWeaponry>();

			foreach (ToolData tool in Data.Tools)
			{
				List<PartData> toolParts = new List<PartData>();
				foreach (string partName in tool.Parts)
				{
					toolParts.Add(Data.Parts.First(part => part.PartName == partName));
				}

				if (toolParts.Count != 2)
				{
					mod.Logger.Warn($"The tool: {tool.ToolName} - does not have 2 parts");
					continue;
				}

				foreach (string firstPartMaterial in toolParts[0].ValidMaterials)
				{
					MaterialData firstMaterial = Data.Materials.First(material => material.MaterialName == firstPartMaterial);

					foreach (string secondPartMaterial in toolParts[1].ValidMaterials)
					{
						MaterialData secondMaterial = Data.Materials.First(material => material.MaterialName == secondPartMaterial);

						// Mix materials
						// Step one, mix textures
						Texture2D firstTexture = mod.GetTexture(firstMaterial.TexturePath);
						Point firstPoint = new Point(firstMaterial.OriginX, firstMaterial.OriginY);
						Texture2D secondTexture = mod.GetTexture(secondMaterial.TexturePath);
						Point secondPoint = new Point(secondMaterial.OriginX, secondMaterial.OriginY);

						Texture2D mixedTexture = null;
						if (firstTexture.Width * firstTexture.Height > secondTexture.Width * secondTexture.Height) // Call depending on which texture is bigger
							mixedTexture = Utilities.MixTexture2D(firstTexture, firstPoint, secondTexture, secondPoint);
						else
							mixedTexture = Utilities.MixTexture2D(secondTexture, secondPoint, firstTexture, firstPoint);

						// Step two, mix stats
						var itemInfo = CreateInfoFromMaterials(firstMaterial, secondMaterial);

						// Step three, add item
						string itemName = $"{tool.ToolName} {firstMaterial.MaterialName}-{secondMaterial.MaterialName}";
						mod.AddItem(itemName, new BaseItem(itemInfo, mixedTexture));
					}
				}
			}
		}

		public ItemInfo CreateInfoFromMaterials(MaterialData firstMaterial, MaterialData secondMaterial)
		{
			// Get the highest modifier
			float modifier = firstMaterial.Modifier > secondMaterial.Modifier
				? firstMaterial.Modifier
				: secondMaterial.Modifier;
			modifier = modifier == 0 ? 1 : modifier; // If it's zero, change it to 1

			object itemInfo = new ItemInfo(); // Store the ItemInfo instance as an object to be able to do "field.SetValue"

			PropertyInfo[] materialProperties = typeof(MaterialData).GetProperties();
			foreach (PropertyInfo materialProperty in materialProperties)
			{
				var fieldNameAttribute = materialProperty.GetCustomAttribute<FieldNameReferenceAttribute>();
				var modifierAttribute = materialProperty.GetCustomAttribute<ModifierTypeAttribute>();

				// Skip fields with no "FieldNameReferenceAttribute" or an empty attribute
				if (fieldNameAttribute == null || string.IsNullOrWhiteSpace(fieldNameAttribute.FieldName))
					continue;

				// Get the stat value
				object firstMaterialValue = materialProperty.GetValue(firstMaterial);
				object secondMaterialValue = materialProperty.GetValue(secondMaterial);
				object biggerValue = CompareObjects(firstMaterialValue, secondMaterialValue) > 0
					? firstMaterialValue
					: secondMaterialValue;

				object finalValue = biggerValue;

				// Calculate the final value, depending on the ModifierType
				switch (modifierAttribute.Type)
				{
					case ModifierTypes.MultiplyFloat:
						finalValue = (float) biggerValue * modifier;
						break;
					case ModifierTypes.MultiplyInt:
						finalValue = (int) ((int) biggerValue * modifier);
						break;
					case ModifierTypes.DivideFloat:
						finalValue = (float) biggerValue / modifier;
						break;
					case ModifierTypes.DivideInt:
						finalValue = (int) ((int) biggerValue / modifier);
						break;
				}

				// Set the value of the ItemInfo field
				var infoField = typeof(ItemInfo).GetField(fieldNameAttribute.FieldName);
				infoField.SetValue(itemInfo, finalValue);
			}

			return (ItemInfo)itemInfo;
		}

		/// <summary>
		/// Compares two <see cref="object"/> that implement <see cref="IComparable"/>
		/// </summary>
		private int CompareObjects(object obj1, object obj2)
		{
			var comparable1 = obj1 as IComparable;
			var comparable2 = obj2 as IComparable;

			if (comparable1 == null || comparable2 == null)
				throw new ArgumentException("The objects passed to CompareObjects must implement IComparable");

			return comparable1.CompareTo(comparable2);
		}
	}
}
