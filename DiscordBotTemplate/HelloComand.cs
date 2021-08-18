using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTemplate
{
	class HelloComand : ModuleBase<SocketCommandContext>
	{
		[Command("hello")]
		[Alias("hi")]
		public async Task SayHello()
		{
			await Context.Channel.SendMessageAsync("Hallo");
		}
	}
}
