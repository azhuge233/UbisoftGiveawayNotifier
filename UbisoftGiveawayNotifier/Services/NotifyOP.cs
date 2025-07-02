using Microsoft.Extensions.Logging;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;
using UbisoftGiveawayNotifier.Services.Notifier;
using UbisoftGiveawayNotifier.Notifier;
using Microsoft.Extensions.Options;

namespace UbisoftGiveawayNotifier.Services {
	internal class NotifyOP(ILogger<NotifyOP> logger, IOptions<Config> config, TgBot tgBot, Barker barker, QQHttp qqHttp, QQWebSocket qqWS, PushPlus pushPlus, DingTalk dingTalk, PushDeer pushDeer, Discord discord, Email email, Meow meow) : IDisposable {
		private readonly ILogger<NotifyOP> _logger = logger;
		private readonly Config config = config.Value;

		public async Task Notify(List<FreeGameRecord> pushList) {
			if (pushList.Count == 0) {
				_logger.LogInformation(NotifyOPString.debugNoNewNotifications);
				return;
			}

			try {
				_logger.LogDebug(NotifyOPString.debugNotify);
				var notifyTasks = new List<Task>();

				// Telegram notifications
				if (config.EnableTelegram) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "Telegram");
					notifyTasks.Add(tgBot.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "Telegram");

				// Bark notifications
				if (config.EnableBark) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "Bark");
					notifyTasks.Add(barker.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "Bark");

				// QQ Http notifications
				if (config.EnableQQHttp) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "QQ Http");
					notifyTasks.Add(qqHttp.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "QQ Http");

				//QQ WebSocket notifications
				if (config.EnableQQWebSocket) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "QQ WebSocket");
					notifyTasks.Add(qqWS.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "QQ WebSocket");

				// PushPlus notifications
				if (config.EnablePushPlus) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "PushPlus");
					notifyTasks.Add(pushPlus.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "PushPlus");

				// DingTalk notifications
				if (config.EnableDingTalk) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "DingTalk");
					notifyTasks.Add(dingTalk.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "DingTalk");

				// PushDeer notifications
				if (config.EnablePushDeer) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "PushDeer");
					notifyTasks.Add(pushDeer.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "PushDeer");

				// Discord notifications
				if (config.EnableDiscord) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "Discord");
					notifyTasks.Add(discord.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "Discord");

				// Email notifications
				if (config.EnableEmail) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "Email");
					notifyTasks.Add(email.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "Email");

				// Meow notifications
				if (config.EnableMeow) {
					_logger.LogInformation(NotifyOPString.debugEnabledFormat, "Meow");
					notifyTasks.Add(meow.SendMessage(pushList));
				} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "Meow");

				await Task.WhenAll(notifyTasks);

				_logger.LogDebug($"Done: {NotifyOPString.debugNotify}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifyOPString.debugNotify}");
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
