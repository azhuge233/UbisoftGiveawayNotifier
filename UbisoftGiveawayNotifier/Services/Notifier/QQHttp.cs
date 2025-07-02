using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.PostContent;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class QQHttp(ILogger<QQHttp> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<QQHttp> _logger = logger;
		private readonly Config config = config.Value;

		private HttpClient Client { get; set; }	= new HttpClient();

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugQQPusherSendMessage);

				string url = string.Format(NotifyFormatString.qqHttpUrlFormat, config.QQHttpAddress, config.QQHttpPort, config.QQHttpToken);

				var sb = new StringBuilder();

				var content = new QQHttpPostContent {
					UserID = config.ToQQID
				};

				var data = new StringContent(string.Empty);
				var resp = new HttpResponseMessage();

				foreach (var record in records) {
					_logger.LogDebug($"{NotifierString.debugQQPusherSendMessage} : {record.Name}");

					content.Message = $"{record.ToQQMessage()}{NotifyFormatString.projectLink}";

					data = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
					resp = await Client.PostAsync(url, data);

					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {NotifierString.debugQQPusherSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {NotifierString.debugQQPusherSendMessage}");
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
