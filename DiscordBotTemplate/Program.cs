using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace DiscordBotTemplate
{
	class Program
	{
		static void Main(string[] args)
		{
			BotManager manager = new BotManager();
			manager.RunBot().GetAwaiter().GetResult();
		}
		
	}
}
