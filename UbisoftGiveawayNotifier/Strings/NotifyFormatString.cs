namespace UbisoftGiveawayNotifier.Strings {
	internal class NotifyFormatString {
		#region record string
		internal const string telegramPushFormat = 
			"<b>Ubisoft</b>\n\n" + 
			"<b>{0}</b>\n" + 
			"Link: <a href=\"{1}\" > {0}</a>\n\n" + 
			"#Ubisoft #{2}";
		internal const string barkPushFormat =
			"{0}\n" +
			"Link: {1}";
		internal const string emailPushHtmlFormat =
			"<p><b>{0}</b><br>" +
			"Link: <a href=\"{1}\" > {0}</a><br>";
		internal const string qqPushFormat =
			imTitle +
			"{0}\n" +
			"Link: {1}";
		internal const string pushPlusPushHtmlFormat =
			"<p><b>{0}</b><br>" +
			"Link: <a href=\"{1}\" > {0}</a><br>";
		internal const string dingTalkPushFormat =
			imTitle +
			"{0}\n" +
			"Link: {1}";
		internal const string pushDeerPushFormat =
			imTitle +
			"{0}\n" +
			"Link: {1}";
		internal const string discordPushFormat =
			"Link: {0}";
		internal const string meowPushFormat =
			"{0}\n" +
			"Link: {1}";
		#endregion

		#region url, title format string
		internal const string imTitle = "Ubisoft\n\n";

		internal const string barkUrlFormat = "{0}/{1}/";
		internal const string barkUrlTitle = "UbisoftGiveawayNotifier/";
		internal const string barkUrlArgs =
			"?group=ubisoftgiveawaynotifier" +
			"&isArchive=1" +
			"&sound=calypso";

		internal const string emailTitleFormat = "{0} new free game(s) - UbisoftGiveawayNotifier";
		internal const string emailBodyFormat = "<br>{0}";

		internal const string qqHttpUrlFormat = "http://{0}:{1}/send_private_msg?access_token={2}";
		internal const string qqWebSocketUrlFormat = "ws://{0}:{1}/?access_token={2}";
		internal const string qqWebSocketSendAction = "send_private_msg";

		internal const string pushPlusTitleFormat = "{0} new free game(s) - UbisoftGiveawayNotifier";
		internal const string pushPlusBodyFormat = "<br>{0}";
		internal const string pushPlusUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";

		internal const string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";

		internal const string pushDeerUrlFormat = "https://api2.pushdeer.com/message/push?pushkey={0}&&text={1}";

		internal const string meowUrlFormat = "{0}/{1}";
		internal const string meowUrlTitle = "UbisoftGiveawayNotifier";
		#endregion

		internal const string projectLink = "\n\nFrom https://github.com/azhuge233/UbisoftGiveawayNotifier";
		internal const string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/UbisoftGiveawayNotifier\">UbisoftGiveawayNotifier</a>";
	}
}
