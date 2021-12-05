﻿using Microsoft.Extensions.Logging;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services {
	internal class ConfigValidator: IDisposable {
		private readonly ILogger<ConfigValidator> _logger;

		public ConfigValidator(ILogger<ConfigValidator> logger) {
			_logger = logger;
		}

		internal void CheckValid(Config config) {
			try {
				_logger.LogDebug(ConfigValidatorString.debugCheckValid);

				//Telegram
				if (config.EnableTelegram) {
					if (string.IsNullOrEmpty(config.TelegramToken))
						throw new Exception(message: "No Telegram Token provided!");
					if (string.IsNullOrEmpty(config.TelegramChatID))
						throw new Exception(message: "No Telegram ChatID provided!");
				}

				//Bark
				if (config.EnableBark) {
					if (string.IsNullOrEmpty(config.BarkAddress))
						throw new Exception(message: "No Bark Address provided!");
					if (string.IsNullOrEmpty(config.BarkToken))
						throw new Exception(message: "No Bark Token provided!");
				}

				//Email
				if (config.EnableEmail) {
					if (string.IsNullOrEmpty(config.FromEmailAddress))
						throw new Exception(message: "No from email address provided!");
					if (string.IsNullOrEmpty(config.ToEmailAddress))
						throw new Exception(message: "No to email address provided!");
					if (string.IsNullOrEmpty(config.SMTPServer))
						throw new Exception(message: "No SMTP server provided!");
					if (string.IsNullOrEmpty(config.AuthAccount))
						throw new Exception(message: "No email auth account provided!");
					if (string.IsNullOrEmpty(config.AuthPassword))
						throw new Exception(message: "No email auth password provided!");
				}

				//QQ
				if (config.EnableQQ) {
					if (string.IsNullOrEmpty(config.QQAddress))
						throw new Exception(message: "No QQ address provided!");
					if (string.IsNullOrEmpty(config.QQPort))
						throw new Exception(message: "No QQ port provided!");
					if (string.IsNullOrEmpty(config.ToQQID))
						throw new Exception(message: "No QQ ID provided!");
				}

				//PushPlus
				if (config.EnablePushPlus) {
					if (string.IsNullOrEmpty(config.PushPlusToken))
						throw new Exception(message: "No PushPlus token provided!");
				}

				//DingTalk
				if (config.EnableDingTalk) {
					if (string.IsNullOrEmpty(config.DingTalkBotToken))
						throw new Exception(message: "No DingTalk token provided!");
				}

				_logger.LogDebug($"Done: {ConfigValidatorString.debugCheckValid}");
			} catch (Exception) {
				_logger.LogError($"Error: {ConfigValidatorString.debugCheckValid}");
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