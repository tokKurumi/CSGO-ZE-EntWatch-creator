using ValveKeyValue;

namespace EntWatch_creator.Models
{
	public class EntWatchCreator
	{
		// TODO:
		// - validate file extension

		private string mapPath;
		private IEnumerable<string> bspMapFile = Enumerable.Empty<string>();
		private int startEntityLine; // Needs for parallel search of first map Entity line position (increases the speed)

		public List<MapEntity> Entities { get; private set; } = new List<MapEntity>();
		public List<EntWatchItem> Items { get; private set; } = new List<EntWatchItem>();

		public EntWatchCreator(string mapPath)
		{
			this.mapPath = mapPath;
			bspMapFile = File.ReadLines(mapPath);

			startEntityLine = bspMapFile.AsParallel()
				.Select((line, lineIndex) => new { line, lineIndex })
				.First(item => item.line.Contains(""""hammerid" "1""""))
				.lineIndex  // Trying to find first row of world Entity
				+ 2;        // Because need skeep closing bracket char
		}

		public async Task WriteEntitiesToFileAsync()
		{
			await WriteEntitiesToFileAsync(Path.GetFileNameWithoutExtension(mapPath) + ".entities");
		}

		public async Task WriteEntitiesToFileAsync(string path)
		{
			if (Entities.Count == 0)
			{
				GenerateEntities();
			}

			await File.WriteAllLinesAsync(path, Entities.Select(property => property.ToString()));
		}

		public void WriteEntWatchToFile()
		{
			WriteEntWatchToFile(Path.GetFileNameWithoutExtension(mapPath) + ".cfg");
		}

		public void WriteEntWatchToFile(string path)
		{
			if (Items.Count == 0)
			{
				GenerateEntWatch();
			}

			using (var writer = File.OpenWrite(path))
			{
				KVSerializer.Create(KVSerializationFormat.KeyValues1Text).Serialize(writer, Items, "entities");
			}
		}

		public void GenerateEntities()
		{
			var entityOpeningFlag = false; // Needs to check if found an Entity
			var currentEntity = new MapEntity();

			foreach (var line in bspMapFile.Skip(startEntityLine))
			{
				if (line == "}")
				{
					if (!entityOpeningFlag)
					{
						break; // Found last Entity
					}

					entityOpeningFlag = false;
					Entities.Add(currentEntity);
					continue;
				}

				if (line == "{")
				{
					if (entityOpeningFlag)
					{
						throw new ArgumentException("Something wrong with bsp file."); // Probably I should generate "correct" exception, but I am too lazy...
					}

					entityOpeningFlag = true;
					currentEntity = new MapEntity();
					continue;
				}

				if (entityOpeningFlag)
				{
					currentEntity.AddProperty(line);
				}
			}
		}

		public void GenerateEntWatch()
		{
			if (Entities.Count == 0)
			{
				GenerateEntities();
			}

			foreach (var entity in Entities)
			{
				var _weapon = entity.Properties.AsParallel()
					.FirstOrDefault(property => (property.Item1 == "classname" && property.Item2.StartsWith("weapon_")))
					.Item2;

				if (_weapon is not null)
				{
					var _name = string.Empty;
					var _shortname = string.Empty;
					var _hammerID = -1;

					foreach (var property in entity.Properties)
					{
						if (_name != string.Empty && _shortname == string.Empty && _hammerID != -1)
						{
							break;
						}

						switch (property.Item1)
						{
							case "targetname":
								{
									_name = property.Item2;
									_shortname = _name;
								}
								break;

							case "hammerid":
								{
									_hammerID = Convert.ToInt32(property.Item2);
								}
								break;
						}
					}

					Items.Add(new EntWatchItem
					(
						name: _name,
						shortname: _shortname,
						hammerID: _hammerID
					));
				}
			}
		}
	}
}