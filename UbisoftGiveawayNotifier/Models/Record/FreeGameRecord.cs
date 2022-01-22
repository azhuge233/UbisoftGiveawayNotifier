using System.Text;
using System.Text.RegularExpressions;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Models.Record {
	public class FreeGameRecord {
		public string? Url { get; set; }

		public string? Name { get; set; }

		private static string RemoveSpecialCharacters(string? str) {
			if (str == null) return string.Empty;
			return Regex.Replace(str, ParseString.removeSpecialCharsRegex, string.Empty);
		}

		internal string ToTelegramMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatString.telegramPushFormat, Name, Url, RemoveSpecialCharacters(Name)).ToString();
		}

		internal string ToBarkMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatString.barkPushFormat, Name, Url).ToString();
		}

		internal string ToDingTalkMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatString.dingTalkPushFormat, Name, Url).ToString();
		}

		internal string ToEmailMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatString.emailPushHtmlFormat, Name, Url).ToString();
		}
		internal string ToPushPlusMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatString.pushPlusPushHtmlFormat, Name, Url).ToString();
		}

		internal string ToQQMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatString.qqPushFormat, Name, Url).ToString();
		}

		internal string ToPushDeerMessage() {
			return new StringBuilder().AppendFormat(NotifyFormatString.pushDeerFormat, Name, Url).ToString();
		}
	}
}
