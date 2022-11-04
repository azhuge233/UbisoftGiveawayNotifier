using Microsoft.Playwright;
using Microsoft.Extensions.Logging;
using UbisoftGiveawayNotifier.Strings;
using UbisoftGiveawayNotifier.Models.Config;

namespace UbisoftGiveawayNotifier.Services {
	internal class Scraper: IDisposable {
		private readonly ILogger<Scraper> _logger;

		public Scraper(ILogger<Scraper> logger) { 
			_logger = logger;
			Microsoft.Playwright.Program.Main(new string[] { "install", "firefox" });
		}

		public async Task<string> GetUbisoftSource(Config config) {
			try {
				_logger.LogDebug($"{ScrapeString.debugGetubisoftSource}");

				using var playwright = await Playwright.CreateAsync();
				await using var browser = await playwright.Firefox.LaunchAsync(new() { Headless = false });

				var context = await browser.NewContextAsync();
				await context.AddCookiesAsync(new[] {
					new Cookie() { Name = ScrapeString.bool_UBI_PRIVACY_POLICY_ACCEPTED, Value = ScrapeString.boolCookieValue, Domain = ScrapeString.giveawayCookieDomain, Path = ScrapeString.cookiePath},
					new Cookie() { Name = ScrapeString.bool_UBI_PRIVACY_POLICY_VIEWED, Value = ScrapeString.boolCookieValue, Domain = ScrapeString.giveawayCookieDomain, Path = ScrapeString.cookiePath},
					// Below cookies are for CA store page store.ubi.com, diabled
					//new Cookie() { Name = ScrapeString.int_dw_cookies_accepted, Value = ScrapeString.intCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath},
					//new Cookie() { Name = ScrapeString.int_dw, Value = ScrapeString.intCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath},
					//new Cookie() { Name = ScrapeString.int_dw_dnt, Value = ScrapeString.intCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath},
					//new Cookie() { Name = ScrapeString.bool_geopopup_ca, Value = ScrapeString.boolCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath},
					//new Cookie() { Name = ScrapeString.string_prefCountry, Value = ScrapeString.countryCookieValue, Domain = ScrapeString.storeCookieDomain, Path = ScrapeString.cookiePath}
				});

				var page = await context.NewPageAsync();
				page.SetDefaultTimeout(config.TimeOutMilliSecond);
				page.SetDefaultNavigationTimeout(config.TimeOutMilliSecond);

				await page.GotoAsync(ScrapeString.UbisoftGiveawayPageUrl);
				await page.WaitForSelectorAsync("div.free-event");
				await page.WaitForLoadStateAsync();

				string result = await page.InnerHTMLAsync("*");

				_logger.LogDebug($"Done: {ScrapeString.debugGetubisoftSource}");
				return result;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeString.debugGetubisoftSource}");
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
