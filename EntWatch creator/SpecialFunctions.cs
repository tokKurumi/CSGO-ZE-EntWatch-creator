using System.Text;

namespace EntWatch_creator
{
	public static class SpecialFunctions
	{
		public static string RemoveSpecialCharacter(this string str, char charToRemove)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in str)
			{
				if(c != charToRemove)
				{
					sb.Append(c);
				}
			}
			return sb.ToString();
		}
	}
}