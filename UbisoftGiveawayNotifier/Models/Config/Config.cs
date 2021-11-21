namespace UbisoftGiveawayNotifier.Models.Config {
	public class Config : NotifyConfig {
		public bool EnableHeadless { get; set; }
		public bool NotifyKeepGamesOnly { get; set; }
		public int TimeOutMilliSecond { get; set; }
	}
}
