using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using UbisoftGiveawayNotifier.Modules;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;
using UbisoftGiveawayNotifier.Services.Notifier;

namespace UbisoftGiveawayNotifier.Services {
	internal class NotifyOP: IDisposable {
		private readonly ILogger<NotifyOP> _logger;
		private readonly IServiceProvider services = DI.BuildDiNotifierOnly();

		public NotifyOP(ILogger<NotifyOP> logger) {
			_logger = logger;
		}

		public async Task Notify(NotifyConfig config, List<FreeGameRecord> pushList) {
			if (pushList.Count == 0) {
				_logger.LogInformation(NotifyOPString.debugNoNewNotifications);
				return;
			}

			try {
				_logger.LogDebug(NotifyOPString.debugNotify);
				using (services as IDisposable) {
					// Telegram notifications
					if (config.EnableTelegram) {
						_logger.LogInformation(NotifyOPString.debugEnabledFormat, "Telegram");
						await services.GetRequiredService<TgBot>().SendMessage(config, pushList);
					} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "Telegram");

					// Bark notifications
					if (config.EnableBark) {
						_logger.LogInformation(NotifyOPString.debugEnabledFormat, "Bark");
						await services.GetRequiredService<Barker>().SendMessage(config, pushList);
					} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "Bark");

					// QQ notifications
					if (config.EnableQQ) {
						_logger.LogInformation(NotifyOPString.debugEnabledFormat, "QQ");
						await services.GetRequiredService<QQPusher>().SendMessage(config, pushList);
					} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "QQ");

					// PushPlus notifications
					if (config.EnablePushPlus) {
						_logger.LogInformation(NotifyOPString.debugEnabledFormat, "PushPlus");
						await services.GetRequiredService<PushPlus>().SendMessage(config, pushList);
					} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "PushPlus");

					// DingTalk notifications
					if (config.EnableDingTalk) {
						_logger.LogInformation(NotifyOPString.debugEnabledFormat, "DingTalk");
						await services.GetRequiredService<DingTalk>().SendMessage(config, pushList);
					} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "DingTalk");

					// PushDeer notifications
					if (config.EnablePushDeer) {
						_logger.LogInformation(NotifyOPString.debugEnabledFormat, "PushDeer");
						await services.GetRequiredService<PushDeer>().SendMessage(config, pushList);
					} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "PushDeer");

					// Email notifications
					if (config.EnableEmail) {
						_logger.LogInformation(NotifyOPString.debugEnabledFormat, "Email");
						await services.GetRequiredService<Email>().SendMessage(config, pushList);
					} else _logger.LogInformation(NotifyOPString.debugDisabledFormat, "Email");
				}

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
