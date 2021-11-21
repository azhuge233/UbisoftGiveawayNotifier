using System.Text;
using System.Web;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class PushPlus: INotifiable {
		private readonly ILogger<PushPlus> _logger;

		public PushPlus(ILogger<PushPlus> logger) {
			_logger = logger;
		}

		private string CreateMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugPushPlusCreateMessage);

				var sb = new StringBuilder();

				records.ForEach(record => {
					sb.AppendFormat(NotifyFormatString.pushPlusBodyFormat, record.ToPushPlusMessage());
				});

				sb.Append("<br>").Append(NotifyFormatString.projectLinkHTML);

				_logger.LogDebug($"Done: {NotifierString.debugPushPlusCreateMessage}");
				return HttpUtility.UrlEncode(sb.ToString());
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierString.debugPushPlusCreateMessage}");
				throw;
			}
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugPushPlusSendMessage);

				var title = HttpUtility.UrlEncode(new StringBuilder().AppendFormat(NotifyFormatString.pushPlusTitleFormat, records.Count).ToString());
				var url = new StringBuilder().AppendFormat(NotifyFormatString.pushPlusUrlFormat, config.PushPlusToken, title);
				var message = CreateMessage(records);

				var resp = await new HtmlWeb().LoadFromWebAsync(
					new StringBuilder()
						.Append(url)
						.Append(message)
						.ToString()
				);
				_logger.LogDebug(resp.Text);

				_logger.LogDebug($"Done: {NotifierString.debugPushPlusSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierString.debugPushPlusSendMessage}");
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
