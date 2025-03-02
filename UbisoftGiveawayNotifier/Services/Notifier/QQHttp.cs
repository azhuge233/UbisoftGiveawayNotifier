using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.PostContent;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class QQHttp: INotifiable {
		private readonly ILogger<QQHttp> _logger;	

		private HttpClient Client { get; set; }	= new HttpClient();

		public QQHttp(ILogger<QQHttp> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugQQPusherSendMessage);

				string url = string.Format(NotifyFormatString.qqUrlFormat, config.QQHttpAddress, config.QQHttpPort, config.QQHttpToken);

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
