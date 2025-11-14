using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Playwright;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services {
	internal class Scraper : IDisposable {
		private readonly ILogger<Scraper> _logger;
		private readonly Config _config;

		public Scraper(ILogger<Scraper> logger, IOptions<Config> config) {
			_logger = logger;
			_config = config.Value;
			Microsoft.Playwright.Program.Main(["install", "firefox"]);
		}

		public async Task<string> GetUbisoftSource() {
			try {
				_logger.LogDebug($"{ScrapeString.debugGetubisoftSource}");

				var page = await GetNewPageInstance();

				await page.GotoAsync(ScrapeString.UbisoftStoreFreeGamesUrl, new() { WaitUntil = WaitUntilState.Load });

				var source = await page.Locator("body").InnerHTMLAsync();

				await page.CloseAsync();

				_logger.LogDebug($"Done: {ScrapeString.debugGetubisoftSource}");
				return source;
			} catch (Exception) {
				_logger.LogError($"Error: {ScrapeString.debugGetubisoftSource}");
				throw;
			} finally {
				Dispose();
			}
		}

		private async Task<IPage> GetNewPageInstance() {
			var playwright = await Playwright.CreateAsync();
			var browser = await playwright.Firefox.LaunchAsync(new() { Headless = _config.EnableHeadless });

			var context = await browser.NewContextAsync();

			await context.AddCookiesAsync([
				new Cookie() { // resolve privacy policy popup
					Name = ScrapeString.bool_UBI_PRIVACY_POLICY_ACCEPTED,
					Value = ScrapeString.boolCookieValue,
					Domain = ScrapeString.storeCookieDomain,
					Path = ScrapeString.cookiePath
				},
				new Cookie() { // resolve privacy policy popup
					Name = ScrapeString.bool_UBI_PRIVACY_POLICY_VIEWED,
					Value = ScrapeString.boolCookieValue,
					Domain = ScrapeString.storeCookieDomain,
					Path = ScrapeString.cookiePath
				},
				new Cookie() { // resolve location popup
					Name = ScrapeString.bool_geopopup_us,
					Value = ScrapeString.boolCookieValue,
					Domain = ScrapeString.storeCookieDomain,
					Path = ScrapeString.cookiePath
				},
				new Cookie() {
					Name = ScrapeString.string_prefCountry,
					Value = ScrapeString.countryCookieValue_us,
					Domain = ScrapeString.storeCookieDomain,
					Path = ScrapeString.cookiePath,
					SameSite = SameSiteAttribute.None
				}
			]);

			var page = await context.NewPageAsync();
			page.SetDefaultTimeout(_config.TimeOutMilliSecond);
			page.SetDefaultNavigationTimeout(_config.TimeOutMilliSecond);
			await page.RouteAsync("**/*", async route => {
				var resourceBlockList = new List<string> { 
					"image", 
					"font" 
				};
				if (resourceBlockList.Contains(route.Request.ResourceType) || !route.Request.Url.Contains(ScrapeString.storeCookieDomain)) await route.AbortAsync();
				else await route.ContinueAsync();
			});

			return page;
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
