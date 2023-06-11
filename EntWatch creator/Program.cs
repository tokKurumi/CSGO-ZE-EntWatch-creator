using EntWatch_creator.Models;

public class Program
{
	public static async Task Main(string[] args)
	{
		var ewc = new EntWatchCreator(args[0]);
		await ewc.WriteEntitiesToFileAsync();
		ewc.WriteEntWatchToFile();
	}
}