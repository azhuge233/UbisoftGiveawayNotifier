using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Notifier;
using UbisoftGiveawayNotifier.Services;
using UbisoftGiveawayNotifier.Services.Notifier;

namespace UbisoftGiveawayNotifier.Modules {
	internal static class DI {
		private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;

		private static readonly IConfigurationRoot logConfig = new ConfigurationBuilder()
		   .SetBasePath(BasePath)
		   .Build();
		private static readonly IConfigurationRoot configuration = new ConfigurationBuilder()
		   .SetBasePath(BasePath)
           .AddJsonFile($"Config File{Path.DirectorySeparatorChar}config.json", optional: false, reloadOnChange: true)
		   .Build();

		internal static IServiceProvider BuildAll() {
            return new ServiceCollection()
               .AddTransient<JsonOP>()
               .AddTransient<ConfigValidator>()
               .AddTransient<Scraper>()
               .AddTransient<Parser>()
               .AddTransient<NotifyOP>()
               .AddTransient<Barker>()
               .AddTransient<TgBot>()
               .AddTransient<Email>()
               .AddTransient<QQHttp>()
			   .AddTransient<QQWebSocket>()
			   .AddTransient<PushPlus>()
               .AddTransient<DingTalk>()
               .AddTransient<PushDeer>()
			   .AddTransient<Discord>()
               .AddTransient<Meow>()
               .Configure<Config>(configuration)
			   .AddLogging(loggingBuilder => {
                   // configure Logging with NLog
                   loggingBuilder.ClearProviders();
                   loggingBuilder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                   loggingBuilder.AddNLog(logConfig);
               })
               .BuildServiceProvider();
        }
    }
}
