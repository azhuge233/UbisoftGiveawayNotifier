﻿using Microsoft.Extensions.Logging;
using UbisoftGiveawayNotifier.Strings;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Models.UbisoftGraphQLJsonData;
using Newtonsoft.Json;
using System.Text.Json;

namespace UbisoftGiveawayNotifier.Services {
	internal class Parser: IDisposable {
		private readonly ILogger<Parser> _logger;

		public Parser(ILogger<Parser> logger) {
			_logger = logger;
		}

		public Tuple<List<FreeGameRecord>, List<FreeGameRecord>> Parse(string source, List<FreeGameRecord> oldRecords) {
			try {
				_logger.LogDebug(ParseString.debugHtmlParser);
				_logger.LogDebug(System.Text.Json.JsonSerializer.Serialize(JsonDocument.Parse(source), new JsonSerializerOptions() { WriteIndented = true }));

				var resultList = new List<FreeGameRecord>();
				var pushList = new List<FreeGameRecord>();

				JsonData data = JsonConvert.DeserializeObject<JsonData>(source);

				if (data != null && !data.Data.PromoMaster.Item.Title.Contains(ParseString.NoPromotionTitle)) {
					string gameName = data.Data.PromoMaster.Item.Title;
					string gameUrl = data.Data.PromoMaster.Item.Content;

					if (data.Data.PromoMaster.Item.ButtonsMaster.Items.First() != null)
						gameUrl = data.Data.PromoMaster.Item.ButtonsMaster.Items.First().LocalizedItems.ButtonUrl;

					_logger.LogInformation(ParseString.infoGameFound, gameName);

					var newFreeGame = new FreeGameRecord() {
						Name = gameName,
						Url = gameUrl
					};

					resultList.Add(newFreeGame);

					if (oldRecords.Count == 0 || !oldRecords.Exists(record => record.Name == gameName && record.Url == gameUrl)) {
						pushList.Add(newFreeGame);
						_logger.LogInformation(ParseString.infoAddToList, gameName);
					} else _logger.LogInformation(ParseString.infoFoundInPreviousRecords, gameName);
				} else _logger.LogInformation(ParseString.debugNoRecordDetected);

				_logger.LogDebug($"Done: {ParseString.debugHtmlParser}");
				return new Tuple<List<FreeGameRecord>, List<FreeGameRecord>>(resultList, pushList);
			} catch (Exception) {
				_logger.LogError($"Error: {ParseString.debugHtmlParser}");
				throw;
			} finally {
				Dispose();
			}
		}

		/// <summary>
		/// old method using Playright
		/// </summary>
		//public Tuple<List<FreeGameRecord>, List<FreeGameRecord>> Parse(string source, List<FreeGameRecord> oldRecords) {
		//	try {
		//		_logger.LogDebug(ParseString.debugHtmlParser);

		//		var resultList = new List<FreeGameRecord>();
		//		var pushList = new List<FreeGameRecord>();
		//		var freePageDoc = new HtmlDocument();

		//		freePageDoc.LoadHtml(source);
		//		var freePageGiveaways = freePageDoc.DocumentNode.SelectNodes(ParseString.giveawayPageFreeGamesXPath);

		//		if (freePageGiveaways != null) {
		//			foreach (var freeGame in freePageGiveaways) {
		//				var aLable = freeGame.SelectSingleNode(ParseString.giveawayPageFreeGameALableXPath);

		//				string gameName = aLable.Attributes[ParseString.giveawayPageFreeGameNameAttr].Value.Split(ParseString.giveawayPageFreeGameNameExtension)[0];
		//				string gameUrl = aLable.Attributes[ParseString.giveawayPageFreeGameUrlAttr].Value;
		//				string freeType = freeGame.SelectSingleNode(ParseString.giveawayPageFreeTypeXPath).InnerText;

		//				if (freeType == ParseString.giveawayPageFreeTypeString) {
		//					_logger.LogInformation(ParseString.infoGameFound, gameName);

		//					var newFreeGame = new FreeGameRecord() {
		//						Name = gameName,
		//						Url = gameUrl
		//					};

		//					resultList.Add(newFreeGame);

		//					if (oldRecords.Count == 0 || !oldRecords.Exists(record => record.Name == gameName && record.Url == gameUrl)) {
		//						pushList.Add(newFreeGame);
		//						_logger.LogInformation(ParseString.infoAddToList, gameName);
		//					} else _logger.LogInformation(ParseString.infoFoundInPreviousRecords, gameName);
		//				}
		//			}
		//		} else _logger.LogInformation(ParseString.debugNoRecordDetected);

		//		_logger.LogDebug($"Done: {ParseString.debugHtmlParser}");
		//		return new Tuple<List<FreeGameRecord>, List<FreeGameRecord>>(resultList, pushList);
		//	} catch (Exception) {
		//		_logger.LogError($"Error: {ParseString.debugHtmlParser}");
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
