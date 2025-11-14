using System.Text;
using System.Text.RegularExpressions;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Models.Record {
	public class FreeGameRecord {
		public string Url { get; set; }

		public string Name { get; set; }

		public string PossibleClaimLink { get; set; }

		private static string RemoveSpecialCharacters(string str) {
			if (str == null) return string.Empty;
			return Regex.Replace(str, ParseString.removeSpecialCharsRegex, string.Empty);
		}

		internal string ToTelegramMessage() {
			return string.Format(NotifyFormatString.telegramPushFormat, Name, Url, PossibleClaimLink, RemoveSpecialCharacters(Name));
		}

		internal string ToBarkMessage() {
			return string.Format(NotifyFormatString.barkPushFormat, Name, Url, PossibleClaimLink);
		}

		internal string ToDingTalkMessage() {
			return string.Format(NotifyFormatString.dingTalkPushFormat, Name, Url, PossibleClaimLink);
		}

		internal string ToEmailMessage() {
			return string.Format(NotifyFormatString.emailPushHtmlFormat, Name, Url, PossibleClaimLink);
		}
		internal string ToPushPlusMessage() {
			return string.Format(NotifyFormatString.pushPlusPushHtmlFormat, Name, Url, PossibleClaimLink);
		}

		internal string ToQQMessage() {
			return string.Format(NotifyFormatString.qqPushFormat, Name, Url, PossibleClaimLink);
		}

		internal string ToPushDeerMessage() {
			return string.Format(NotifyFormatString.pushDeerPushFormat, Name, Url, PossibleClaimLink);
		}

		internal string ToDiscordMessage() {
			return string.Format(NotifyFormatString.discordPushFormat, Url, PossibleClaimLink);
		}

		internal string ToMeowMessage() {
			return string.Format(NotifyFormatString.meowPushFormat, Name, Url, PossibleClaimLink);
		}
	}
}
