using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class PushDeer(ILogger<PushDeer> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<PushDeer> _logger = logger;
		private readonly Config config = config.Value;

		private HttpClient Client { get; set; } = new HttpClient();

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugPushDeerSendMessage);

				foreach (var record in records) {
					_logger.LogDebug($"{NotifierString.debugPushDeerSendMessage} : {record.Name}");
					var resp = await Client.GetAsync(
						new StringBuilder()
						.AppendFormat(NotifyFormatString.pushDeerUrlFormat,
									config.PushDeerToken,
									HttpUtility.UrlEncode(record.ToPushDeerMessage()))
						.Append(HttpUtility.UrlEncode(NotifyFormatString.projectLink))
						.ToString()
					);
					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {NotifierString.debugPushDeerSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierString.debugPushDeerSendMessage}");
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
