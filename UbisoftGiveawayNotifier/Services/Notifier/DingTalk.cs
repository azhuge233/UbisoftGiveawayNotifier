using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UbisoftGiveawayNotifier.Models;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class DingTalk: INotifiable {
		private readonly ILogger<DingTalk> _logger;

		public DingTalk(ILogger<DingTalk> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugDingTalkSendMessage);

				var url = new StringBuilder().AppendFormat(NotifyFormatString.dingTalkUrlFormat, config.DingTalkBotToken).ToString();
				var content = new DingTalkPostContent();

				var client = new HttpClient();
				var data = new StringContent("");
				var resp = new HttpResponseMessage();

				foreach (var record in records) {
					content.text.content = $"{record.ToDingTalkMessage()}{NotifyFormatString.projectLink}";
					data = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
					resp = await client.PostAsync(url, data);
					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {NotifierString.debugDingTalkSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierString.debugDingTalkSendMessage}");
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
