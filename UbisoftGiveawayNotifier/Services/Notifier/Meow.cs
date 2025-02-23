using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.PostContent;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class Meow: INotifiable {
		private readonly ILogger<Meow> _logger;

		public Meow(ILogger<Meow> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugMeowSendMessage);

				var url = string.Format(NotifyFormatString.meowUrlFormat, config.MeowAddress, config.MeowNickname);

				var content = new MeowPostContent() {
					Title = NotifyFormatString.meowUrlTitle
				};

				var client = new HttpClient();

				foreach (var record in records) {
					content.Message = $"{record.ToMeowMessage()}{NotifyFormatString.projectLink}";
					content.Url = record.Url;

					var data = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
					var resp = await client.PostAsync(url, data);

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
					await Task.Delay(3000); // rate limit
				}

				_logger.LogDebug($"Done: {NotifierString.debugMeowSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierString.debugMeowSendMessage}");
			} finally {
				Dispose();
			}
		}

		public void Dispose() { 
			GC.SuppressFinalize(this);
		}
	}
}
