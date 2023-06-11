using System.Text;

namespace EntWatch_creator.Models
{
	public struct MapEntity
	{
		public List<(string, string)> Properties; // Using List instead of Dictionary for increase add new item speed

		public MapEntity()
		{
			Properties = new List<(string, string)>();
		}

		public void AddProperty(string propertyName, string propertyValue)
		{
			Properties.Add((propertyName, propertyValue));
		}

		public void AddProperty(string mapProperty)
		{
			var tmp = mapProperty.RemoveSpecialCharacter('"').Split(' ', 2);
			AddProperty(tmp[0], tmp[1]);
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.AppendLine("{");
			Properties.ForEach(item =>
			{
				sb.AppendFormat("\"{0}\" \"{1}\"\n", item.Item1, item.Item2);
			});
			sb.AppendLine("}");

			return sb.ToString();
		}
	}
}