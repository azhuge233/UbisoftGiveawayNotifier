namespace UbisoftGiveawayNotifier.Models.Config {
	internal class Config : NotifyConfig {
		internal bool EnableHeadless { get; set; }
		internal bool NotifyKeepGamesOnly { get; set; }
		internal int TimeOutMilliSecond { get; set; }
	}
}
