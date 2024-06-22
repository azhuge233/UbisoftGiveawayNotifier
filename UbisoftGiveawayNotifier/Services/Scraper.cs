using Microsoft.Extensions.Logging;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services {
	internal class Scraper: IDisposable {
		private readonly ILogger<Scraper> _logger;

		internal HttpClient Client { get; set; } = new HttpClient();

		public Scraper(ILogger<Scraper> logger) { 
			_logger = logger;

			Client.DefaultRequestHeaders.UserAgent.ParseAdd(ScrapeString.HEADER_VALUE_UA[new Random().Next(0, ScrapeString.HEADER_VALUE_UA.Count - 1)]);
			// Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(ScrapeString.HEADER_VALUE_Authorization);
			Client.DefaultRequestHeaders.Add(ScrapeString.HEADER_KEY_Authorization, ScrapeString.HEADER_VALUE_Authorization);
			Client.DefaultRequestHeaders.Add(ScrapeString.HEADER_KEY_AppID, ScrapeString.HEADER_VALUE_AppID);
			Client.DefaultRequestHeaders.Add(ScrapeString.HEADER_KEY_AppName, ScrapeString.HEADER_VALUE_AppName);
			// Microsoft.Playwright.Program.Main(["install", "firefox"]);
		}

		public async Task<string> GetUbisoftSource() {
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

		/// <summary>
		/// old playwright method
		/// </summary>
		//public async Task<string> GetUbisoftSource(Config config) {
		//	try {
		//		_logger.LogDebug($"{ScrapeString.debugGetubisoftSource}");

		//		using var playwright = await Playwright.CreateAsync();
		//		await using var browser = await playwright.Firefox.LaunchAsync(new() { Headless = config.EnableHeadless });

		//		var context = await browser.NewContextAsync();
		//		await context.AddCookiesAsync([
		//			new Cookie() { Name = ScrapeString.bool_UBI_PRIVACY_POLICY_ACCEPTED, Value = ScrapeString.boolCookieValue, Domain = ScrapeString.giveawayCookieDomain, Path = ScrapeString.cookiePath},
		//			new Cookie() { Name = ScrapeString.bool_UBI_PRIVACY_POLICY_VIEWED, Value = ScrapeString.boolCookieValue, Domain = ScrapeString.giveawayCookieDomain, Path = ScrapeString.cookiePath},
		//			// Below cookies are for CA store page store.ubi.com, diabled
		//			//new Cookie() { Name = ScrapeString.int_dw_cookies_accepted, Value = ScrapeString.intCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath},
		//			//new Cookie() { Name = ScrapeString.int_dw, Value = ScrapeString.intCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath},
		//			//new Cookie() { Name = ScrapeString.int_dw_dnt, Value = ScrapeString.intCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath},
		//			//new Cookie() { Name = ScrapeString.bool_geopopup_ca, Value = ScrapeString.boolCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath},
		//			//new Cookie() { Name = ScrapeString.string_prefCountry, Value = ScrapeString.countryCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath}
		//		]);

		//		var page = await context.NewPageAsync();
		//		page.SetDefaultTimeout(config.TimeOutMilliSecond);
		//		page.SetDefaultNavigationTimeout(config.TimeOutMilliSecond);
		//		await page.RouteAsync("**/*", async route => {
		//			var blockList = new List<string> { "image", "font" };
		//			if (blockList.Contains(route.Request.ResourceType)) await route.AbortAsync();
		//			else await route.ContinueAsync();
		//		});

		//		await page.GotoAsync(ScrapeString.UbisoftGiveawayPageUrl);
		//		await page.WaitForSelectorAsync("div.free-event");
		//		await page.WaitForLoadStateAsync();

		//		string result = await page.InnerHTMLAsync("*");

		//		_logger.LogDebug($"Done: {ScrapeString.debugGetubisoftSource}");
		//		return result;
		//	} catch (Exception) {
		//		_logger.LogError($"Error: {ScrapeString.debugGetubisoftSource}");
		//		throw;
		//	} finally {
		//		Dispose();
		//	}
		//}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
