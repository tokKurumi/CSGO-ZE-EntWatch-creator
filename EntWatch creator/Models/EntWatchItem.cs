using System.Text;
using System.Text.Json.Serialization;
using ValveKeyValue;

namespace EntWatch_creator.Models
{
	/// <remarks>
	/// https://github.com/darkerz7/EntWatch_DZ/blob/master/cfg/sourcemod/entwatch/maps/template.txt
	/// </remarks>
	public struct EntWatchItem
	{
		// Project library can not be used for bool parameters to serialize as valve key-value, so made them strings
		[KVProperty("name")]
		public string Name { get; set; }                            // String, FullName of Item (Chat)
		[KVProperty("shortname")]
		public string ShortName { get; set; }                       // String, ShortName of Item (Hud)
		[KVProperty("color")]
		public string Color { get; set; } = "{default}";            // String, One of the colors. (Chat, Glow)
		[KVProperty("buttonclass")]
		public string ButtonClass { get; set; } = "func_button";    // String, Button Class, Can use "game_ui" for Right Click activation method
		[KVProperty("filtername")]
		public string FilterName { get; set; } = String.Empty;      // String, Filter for Activator
		[KVProperty("blockpickup")]
		public string BlockPickUp { get; set; } = "false";          // Bool, The item cannot be picked up
		[KVProperty("allowtransfer")]
		public string AllowTransfer { get; set; } = "true";         // Bool, Allow admins to transfer an item
		[KVProperty("chat")]
		public string Chat { get; set; } = "false";                 // Bool, Display chat items
		[KVProperty("chatuses")]
		public string ChatUses { get; set; } = "true";              // Bool, Display chat someone is using an item(if disabled chat)
		[KVProperty("hud")]
		public string Hud { get; set; } = "true";                   // Bool, Display Hud items
		[KVProperty("hammerid")]
		public int HammerID { get; set; }                           // Integer, weapon_* HammerID
		[KVProperty("mode")]
		public int Mode { get; set; } = 0;                          // Integer, Mode for item. 0 = Can hold E, 1 = Spam protection only, 2 = Cooldowns, 3 = Limited uses, 4 = Limited uses with cooldowns, 5 = Cooldowns after multiple uses, 6 = Counter - stops when minimum is reached, 7 = Counter - stops when maximum is reached
		[KVProperty("maxuses")]
		public int MaxUses { get; set; } = 0;                       // Integer, Maximum uses for modes 3, 4, 5
		[KVProperty("cooldown")]
		public int Cooldown { get; set; } = 60;							// Integer, Cooldown of item for modes 2, 4, 5

		public EntWatchItem(string name, string shortname, int hammerID)
		{
			Name = name;
			ShortName = shortname;
			HammerID = hammerID;
		}
	}
}
