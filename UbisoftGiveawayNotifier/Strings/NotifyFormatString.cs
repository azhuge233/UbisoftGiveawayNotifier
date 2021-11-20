namespace UbisoftGiveawayNotifier.Strings {
	internal class NotifyFormatString {
		#region record ToMessage(update = false) string
		internal static readonly string telegramPushFormat =
			"<b>{0}</b>\n\n" +
			"Sub/App ID: <i>{1}</i>\n" +
			"免费类型: {2}\n" +
			"链接: <a href=\"{3}\" > {0}</a>\n" +
			"开始时间: {4}\n" +
			"结束时间: {5}\n\n" +
			"#Steam #{2} #{6}";
		internal static readonly string barkPushFormat =
			"{0}\n" +
			"Sub/App ID: {1}\n" +
			"免费类型: {2}\n" +
			"链接: {3}\n" +
			"开始时间: {4}\n" +
			"结束时间: {5}";
		internal static readonly string emailPushHtmlFormat =
			"<p><b>{0}</b><br>" +
			"Sub/App ID: {1}<br>" +
			"免费类型: {2}<br>" +
			"链接: <a href=\"{3}\" > {0}</a><br>" +
			"开始时间: {4}<br>" +
			"结束时间: {5}</p>";
		internal static readonly string qqPushFormat =
			"{0}\n" +
			"Sub/App ID: {1}\n" +
			"免费类型: {2}\n" +
			"链接: {3}\n" +
			"开始时间: {4}\n" +
			"结束时间: {5}";
		internal static readonly string pushPlusPushHtmlFormat =
			"<p><b>{0}</b><br>" +
			"Sub/App ID: {1}<br>" +
			"免费类型: {2}<br>" +
			"链接: <a href=\"{3}\" > {0}</a><br>" +
			"开始时间: {4}<br>" +
			"结束时间: {5}</p>";
		internal static readonly string dingTalkPushFormat =
			"{0}\n" +
			"Sub/App ID: {1}\n" +
			"免费类型: {2}\n" +
			"链接: {3}\n" +
			"开始时间: {4}\n" +
			"结束时间: {5}";
		#endregion


		#region url, title format string
		internal static readonly string barkUrlFormat = "{0}/{1}/";
		internal static readonly string barkUrlTitle = "UbisoftGiveawayNotifier/";
		internal static readonly string barkUrlArgs =
			"?group=steamdbfreegames" +
			"&copy={0}" +
			"&isArchive=1" +
			"&sound=calypso";

		internal static readonly string emailTitleFormat = "{0} new free game(s) - UbisoftGiveawayNotifier";
		internal static readonly string emailBodyFormat = "<br>{0}";

		internal static readonly string qqUrlFormat = "http://{0}:{1}/send_private_msg?user_id={2}&message=";

		internal static readonly string pushPlusTitleFormat = "{0} new free game(s) - UbisoftGiveawayNotifier";
		internal static readonly string pushPlusBodyFormat = "<br>{0}";
		internal static readonly string pushPlusUrlFormat = "http://www.pushplus.plus/send?token={0}&template=html&title={1}&content=";

		internal static readonly string dingTalkUrlFormat = "https://oapi.dingtalk.com/robot/send?access_token={0}";
		#endregion


		internal const string projectLink = "\n\nFrom https://github.com/azhuge233/UbisoftGiveawayNotifier";
		internal const string projectLinkHTML = "<br><br>From <a href=\"https://github.com/azhuge233/UbisoftGiveawayNotifier\">UbisoftGiveawayNotifier</a>";
	}
}
