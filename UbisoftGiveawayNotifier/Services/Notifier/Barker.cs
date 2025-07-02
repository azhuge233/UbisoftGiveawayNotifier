using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Web;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class Barker(ILogger<Barker> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<Barker> _logger = logger;
		private readonly Config config = config.Value;

		private HttpClient Client { get; set; } = new HttpClient();

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				var sb = new StringBuilder();
				string url = new StringBuilder().AppendFormat(NotifyFormatString.barkUrlFormat, config.BarkAddress, config.BarkToken).ToString();

				foreach (var record in records) {
					_logger.LogDebug($"{NotifierString.debugBarkerSendMessage} : {record.Name}");
					var resp = await Client.GetAsync(
						new StringBuilder()
							.Append(url)
							.Append(NotifyFormatString.barkUrlTitle)
							.Append(HttpUtility.UrlEncode(record.ToBarkMessage()))
							.Append(HttpUtility.UrlEncode(NotifyFormatString.projectLink))
							.Append(NotifyFormatString.barkUrlArgs)
							.ToString()
					);
					_logger.LogDebug(await resp.Content.ReadAsStringAsync());
				}

				_logger.LogDebug($"Done: {NotifierString.debugBarkerSendMessage}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierString.debugBarkerSendMessage}");
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
