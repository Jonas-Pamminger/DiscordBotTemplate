using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTemplate
{
	class BotManager
	{
		public static DiscordSocketClient BotClient;

		public static CommandService Commans;
		public static IServiceProvider ServiceProvider;
		public const string PREFIX = "?";



		public async Task RunBot()
		{
			Commans = new CommandService();
			ServiceProvider = ConfigureServices();

			BotClient = new DiscordSocketClient();
			await BotClient.LoginAsync(Discord.TokenType.Bot, "ODc2ODI0MzQ5NTEwODY0ODk2.YRpsTw.1-fnwbxEa5VWvIG51oYEJcgzKuk");
			await BotClient.StartAsync();

			BotClient.Log += BotHasLoggedSomething;
			BotClient.Ready += BotIsReady;
			
			await Task.Delay(-1);
		}

		public IServiceProvider ConfigureServices()
		{
			return new ServiceCollection()
				.AddSingleton<HelloComand>()
				//Add here if multiple commands
				.BuildServiceProvider();
		}

		public Task BotHasLoggedSomething(LogMessage logMessage)
		{
			Console.WriteLine(logMessage);
			return Task.CompletedTask;
		}
		public async Task BotIsReady()
		{
			await Commans.AddModulesAsync(Assembly.GetEntryAssembly(), ServiceProvider);
			await BotClient.SetGameAsync("mit den Nerfen des Programmierers");
			BotClient.MessageReceived += MessageRececived;
		}
		public async Task MessageRececived(SocketMessage arg)
		{
			SocketUserMessage message = arg as SocketUserMessage;
			int comandsposition = 0;
			if (message.HasStringPrefix(PREFIX, ref comandsposition))
			{
				SocketCommandContext context = new SocketCommandContext(BotClient, message);
				IResult restult = await Commans.ExecuteAsync(context, comandsposition, ServiceProvider);
				if (!restult.IsSuccess)
				{
					Console.WriteLine(restult.ErrorReason);
				}
			}
		}
	}
}
