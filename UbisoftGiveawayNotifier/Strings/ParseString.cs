namespace UbisoftGiveawayNotifier.Strings {
	internal class ParseString {
		#region json related
		internal const string NoPromotionTitle = "There are no special events available at the moment";
		#endregion

		#region XPaths
		internal const string liXPath = ".//div[@id='main']//div[@class='product-grid-container']//div[contains(@class, 'search-result-content')]//div[@class='samples']//ul//li[contains(@class, 'grid-tile')]";
		internal const string giveawaySpanXPath = ".//span[contains(@class, 'giveaway')]";
		internal const string gameTitleDivXPath = ".//div[@class='prod-title']";
		internal const string gameUrlAXPath = ".//a[@class='thumb-link']";
		#endregion

		#region Store Page Related
		internal const string storePageFreeGameUrlAttr = "href";
		#endregion

		internal const string removeSpecialCharsRegex = @"[^0-9a-zA-Z]+";
		internal const string storeRootUrl = "https://store.ubi.com";
		internal const string possibleClaimLinkFormat = "https://register.ubisoft.com/{0}_Free/en-GB";

		#region debug strings
		internal const string debugHtmlParser = "Parse";
		internal const string debugNoGiveawayDetected = "No giveaway detected!";
		internal const string infoGameFound = "Found game: {gameName}.";
		internal const string infoAddToList = "Added game {gameName} to list";
		internal const string infoFoundInPreviousRecords = "{gameName} is found in previous records, stop adding in list";
		#endregion
	}
}
