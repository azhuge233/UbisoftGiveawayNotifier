using Microsoft.Extensions.Logging;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services {
	internal class Scraper(ILogger<Scraper> logger) : IDisposable {
		private readonly ILogger<Scraper> _logger = logger;

		internal HttpClient Client { get; set; } = new HttpClient();

		public async Task<string> GetUbisoftSource() {
			InitHttpClient();

			try {
				_logger.LogDebug($"{ScrapeString.debugGetubisoftSource}");

				var response = await Client.GetAsync(ScrapeString.UbisoftPromotionGraphQLRequestUrl);
				var result = await response.Content.ReadAsStringAsync();

				_logger.LogDebug($"Done: {ScrapeString.debugGetubisoftSource}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeString.debugGetubisoftSource}");
				throw;
			} finally {
				Dispose();
			}
		}

		private void InitHttpClient() {
			Client.DefaultRequestHeaders.UserAgent.ParseAdd(ScrapeString.HEADER_VALUE_UA[new Random().Next(0, ScrapeString.HEADER_VALUE_UA.Count - 1)]);
			// Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ScrapeString.HEADER_VALUE_Authorization);
			Client.DefaultRequestHeaders.Add(ScrapeString.HEADER_KEY_Authorization, ScrapeString.HEADER_VALUE_Authorization);
			Client.DefaultRequestHeaders.Add(ScrapeString.HEADER_KEY_AppID, ScrapeString.HEADER_VALUE_AppID);
			Client.DefaultRequestHeaders.Add(ScrapeString.HEADER_KEY_AppName, ScrapeString.HEADER_VALUE_AppName);
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
