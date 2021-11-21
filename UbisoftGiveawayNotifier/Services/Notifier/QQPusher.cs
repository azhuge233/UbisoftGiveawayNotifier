using System.Text;
using System.Web;
using Microsoft.Extensions.Logging;
using HtmlAgilityPack;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class QQPusher: INotifiable {
		private readonly ILogger<QQPusher> _logger;	

		public QQPusher(ILogger<QQPusher> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugQQPusherSendMessage);

				string url = new StringBuilder().AppendFormat(NotifyFormatString.qqUrlFormat, config.QQAddress, config.QQPort, config.ToQQID).ToString();
				var sb = new StringBuilder();
				var webGet = new HtmlWeb();
				var resp = new HtmlDocument();

				foreach (var record in records) {
					_logger.LogDebug($"{NotifierString.debugQQPusherSendMessage} : {record.Name}");
					resp = await webGet.LoadFromWebAsync(
						new StringBuilder()
							.Append(url)
							.Append(HttpUtility.UrlEncode(record.ToQQMessage()))
							.Append(HttpUtility.UrlEncode(NotifyFormatString.projectLink))
							.ToString()
					);
					_logger.LogDebug(resp.Text);
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
