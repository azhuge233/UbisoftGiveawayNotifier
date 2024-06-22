namespace UbisoftGiveawayNotifier.Strings {
	internal class ParseString {
		#region json related
		internal const string NoPromotionTitle = "There are no special events available at the moment";
		#endregion

		#region XPaths
		//internal const string giveawayPageFreeGamesXPath = ".//div[@class=\'free-event\']";
		//internal const string storePageFreeGamesXPath = ".//div[@class=\'product-tile card\']";

		//internal const string giveawayPageFreeTypeXPath = ".//div[@class=\'free-event-type\']//span";
		//internal const string giveawayPageFreeGameALableXPath = ".//a";

		//internal const string storePageFreeGameALableXPath = ".//a[@class=\'thumb-link\']";
		//internal const string storePageFreeGameNameXPath = ".//div[@class=\'prod-title\']";
		//internal const string storePageFreeGameBannerXPath = ".//div[@class=\'card-label giveaway\']";
		#endregion

		#region Giveaway Page Related
		//internal const string giveawayPageFreeTypeString = "Game giveaway";
		//internal const string giveawayPageFreeGameUrlAttr = "data-url";
		//internal const string giveawayPageFreeGameNameAttr = "data-name";
		//internal const string giveawayPageFreeGameNameExtension = "ignt";
		#endregion

		#region Store Page Related
		internal const string storePageFreeGameUrlAttr = "href";
		#endregion

		internal const string removeSpecialCharsRegex = @"[^0-9a-zA-Z]+";
		internal const string storeRootUrlFormat = "https://store.ubi.com{0}";

		#region debug strings
		internal const string debugHtmlParser = "Parse";
		internal const string debugNoRecordDetected = "No record detected!";
		internal const string infoGameFound = "Found game: {gameName}.";
		internal const string infoAddToList = "Added game {gameName} to list";
		internal const string infoFoundInPreviousRecords = "{gameName} is found in previous records, stop adding in list";
		#endregion
	}
}
