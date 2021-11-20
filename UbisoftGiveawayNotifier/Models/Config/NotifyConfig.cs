namespace UbisoftGiveawayNotifier.Models.Config {
	internal class NotifyConfig {
		internal bool EnableTelegram { get; set; }
		internal bool EnableBark { get; set; }
		internal bool EnableEmail { get; set; }

		internal string? TelegramToken { get; set; }
		internal string? TelegramChatID { get; set; }

		internal string? BarkAddress { get; set; }
		internal string? BarkToken { get; set; }

		internal string? SMTPServer { get; set; }
		internal int SMTPPort { get; set; }
		internal string? FromEmailAddress { get; set; }
		internal string? ToEmailAddress { get; set; }
		internal string? AuthAccount { get; set; }
		internal string? AuthPassword { get; set; }

		internal bool EnableQQ { get; set; }
		internal string? QQAddress { get; set; }
		internal string? QQPort { get; set; }
		internal string? ToQQID { get; set; }

		internal bool EnablePushPlus { get; set; }
		internal string? PushPlusToken { get; set; }

		internal bool EnableDingTalk { get; set; }
		internal string? DingTalkBotToken { get; set; }
	}
}
