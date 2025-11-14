namespace UbisoftGiveawayNotifier.Strings {
	internal class ScrapeString {
		#region debug strings
		internal const string debugGetubisoftSource = "Get Ubisoft page source";
		#endregion

		#region Url related
		internal static string UbisoftStoreFreeGamesUrl = "https://store.ubisoft.com/us/free-games";
		// internal static string UbisoftPromotionGraphQLRequestUrl = $"https://cms-cache.ubisoft.com/GraphQL/content/v1/spaces/p0f8o8d25gmk?query=query%20Promo($contentId:String!$locale:String$preview:Boolean)%7BpromoMaster(id:$contentId,preview:$preview)%7Bsys%7Bid%7Ditem:localizedItems(locale:$locale)%7B...on%20Promo%7B...PromoFragment%7D%7D%7D%7Dfragment%20PromoFragment%20on%20Promo%7Bsys%7Bid%7Dtitle%20subtitle%20content%20buttonsMaster:buttonsCollection(limit:12)%7Bitems%7B...on%20ButtonMaster%7B...ButtonMasterFragment%7D%7D%7D%7Dfragment%20ButtonMasterFragment%20on%20ButtonMaster%7Bsys%7Bid%7DlocalizedItems%7B...ButtonFragment%7D%7Dfragment%20ButtonFragment%20on%20Button%7Bsys%7Bid%7DbuttonText%20buttonUrl%7D&variables=%7B%22contentId%22:%22{ContentID}%22,%22locale%22:%22en-US%22,%22preview%22:false,%22tag%22:%22BR-ubisoft%20GA-homescreen%22,%22websiteIdentifier%22:%22Ubisoftcom%22%7D";
		// internal static string UbisoftPromotionGraphQLRequestUrl = $"https://cms-cache.ubisoft.com/GraphQL/content/v1/spaces/p0f8o8d25gmk?query=query%20Promo(%20$contentId:%20String!%20$locale:%20String%20$preview:%20Boolean%20)%20%7B%20promoMaster(id:%20$contentId,%20preview:%20$preview)%20%7B%20sys%20%7B%20id%20%7D%20trackingLocationDetail%20trackingLocation%20impressionTracking%20showCopyToClipboard%20item:%20localizedItems(locale:%20$locale)%20%7B%20...%20on%20Promo%20%7B%20...PromoFragment%20%7D%20%7D%20%7D%20%7D%20fragment%20PromoFragment%20on%20Promo%20%7B%20sys%20%7B%20id%20%7D%20title%20subtitle%20content%20%7D&variables=%7B%22contentId%22:%22{ContentID}%22,%22locale%22:%22en-US%22,%22preview%22:false,%22tag%22:%22BR-ubisoft%20GA-homescreen%22,%22websiteIdentifier%22:%22Ubisoftcom%22%7D";
		internal const string ContentID = "6ZKFBv4inpRkAWcvNjbHdQ";
		// internal const string UbisoftGiveawayPageUrl = "https://www.ubisoft.com/en-us/games/free";
		// internal const string UbisoftGiveawayPageUrl = "https://free.ubisoft.com/";
		// internal const string UbisoftStoreGiveawayUrl = "https://store.ubi.com/ca/free-pc-games/#cgid=free-offers&prefn1=freeOfferProductType&prefv1=Giveaway";
		#endregion

		#region Headers Key
		internal const string HEADER_KEY_Authorization = "Authorization";
		internal const string HEADER_KEY_AppID = "Ubi-Appid";
		internal const string HEADER_KEY_AppName = "Ubi-Appname";
		#endregion

		#region Headers Value
		internal const string HEADER_VALUE_Authorization = "Bearer GfkJig-hk-h8I9QINtjXvRTtgD3fRwVChn48Kik0eqg";
		internal const string HEADER_VALUE_AppID = "f35adcb5-1911-440c-b1c9-48fdc1701c68";
		internal const string HEADER_VALUE_AppName = "Ubisoftcom";
		internal static List<string> HEADER_VALUE_UA = [ 
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36 Edg/126.0.0.0",
			"Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36",
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:126.0) Gecko/20100101 Firefox/126.0",
			"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/124.0.0.0 Safari/537.36 OPR/110.0.0.0",
			"Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/17.5 Safari/605.1.15"
		];
		#endregion

		#region Cookie names
		internal const string bool_UBI_PRIVACY_POLICY_ACCEPTED = "UBI_PRIVACY_POLICY_ACCEPTED";
		internal const string bool_UBI_PRIVACY_POLICY_VIEWED = "UBI_PRIVACY_POLICY_VIEWED";
		internal const string int_dw_cookies_accepted = "dw_cookies_accepted";
		internal const string int_dw = "dw";
		internal const string int_dw_dnt = "dw_dnt";
		internal const string bool_geopopup_ca = "geopopup-ca";
		internal const string bool_geopopup_us = "geopopup-us";
		internal const string string_prefCountry = "prefCountry";
		#endregion

		#region Cookie related
		internal const string giveawayCookieDomain = ".ubisoft.com";
		internal const string storeCookieDomain = "store.ubisoft.com";
		internal const string cookiePath = "/";
		internal const string intCookieValue = "1";
		internal const string boolCookieValue = "true";
		internal const string countryCookieValue_ca = "ca";
		internal const string countryCookieValue_us = "us";
		#endregion
	}
}
