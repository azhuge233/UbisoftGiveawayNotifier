using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.PostContent;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Services.Notifier;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Notifier {
	public class Discord : INotifiable {
		private readonly ILogger<Discord> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notification to Discord";
		#endregion

		public Discord(ILogger<Discord> logger) {
			_logger = logger;
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var url = config.DiscordWebhookURL;
				var content = new DiscordPostContent() {
					Content = records.Count > 1 ? "New Free Games - Ubisoft" : "New Free Game - Ubisoft"
				};

				foreach (var record in records) {
					content.Embeds.Add(
						new Embed() {
							Title = record.Name,
							Url = record.Url,
							Description = record.ToDiscordMessage(),
							Footer = new Footer() { Text = NotifyFormatString.projectLink }
						}
					);
				}

				var data = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
				var resp = await new HttpClient().PostAsync(url, data);
				_logger.LogDebug(await resp.Content.ReadAsStringAsync());

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogError($"Error: {debugSendMessage}");
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
