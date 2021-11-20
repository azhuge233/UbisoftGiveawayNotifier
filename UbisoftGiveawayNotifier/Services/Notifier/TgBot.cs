using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class TgBot: INotifiable {
		private readonly ILogger<TgBot> _logger;

		public TgBot(ILogger<TgBot> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			var sb = new StringBuilder();
			var BotClient = new TelegramBotClient(token: config.TelegramToken);

			try {
				foreach (var record in records) {
					_logger.LogDebug($"{NotifierString.debugTelegramSendMessage} : {record.Name}");
					await BotClient.SendTextMessageAsync(
						chatId: config.TelegramChatID,
						text: $"{record.ToTelegramMessage(update: record.IsUpdate)}{NotifyFormatStrings.projectLinkHTML.Replace("<br>", "\n")}",
						parseMode: ParseMode.Html
					);
					sb.Append(sb.Length == 0 ? record.ID : $",{record.ID}");
				}

				await BotClient.SendTextMessageAsync(
						chatId: config.TelegramChatID,
						text: sb.ToString(),
						parseMode: ParseMode.Html
				);

				_logger.LogDebug($"Done: {NotifierString.debugTelegramSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierString.debugTelegramSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}
		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
