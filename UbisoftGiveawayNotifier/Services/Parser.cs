using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services {
	internal class Parser(ILogger<Parser> logger) : IDisposable {
		private readonly ILogger<Parser> _logger = logger;

		public Tuple<List<FreeGameRecord>, List<FreeGameRecord>> Parse(string source, List<FreeGameRecord> oldRecords) {
			try {
				_logger.LogDebug(ParseString.debugHtmlParser);

				var resultList = new List<FreeGameRecord>();
				var pushList = new List<FreeGameRecord>();

				var htmlDoc = new HtmlDocument();
				htmlDoc.LoadHtml(source);

				var lis = htmlDoc.DocumentNode.SelectNodes(ParseString.liXPath).ToList();

				var giveawaylis = lis.Where(li => li.SelectSingleNode(ParseString.giveawaySpanXPath) != null).ToList();

				if (giveawaylis.Count > 0) {
					foreach (var li in giveawaylis) {
						string gameName = li.SelectSingleNode(ParseString.gameTitleDivXPath).InnerText.Trim();
						string gameUrlPath = li.SelectSingleNode(ParseString.gameUrlAXPath).Attributes[ParseString.storePageFreeGameUrlAttr].Value.Split('?').First();
						string gameUrlFull = $"{ParseString.storeRootUrl}{gameUrlPath}";

						_logger.LogInformation(ParseString.infoGameFound, gameName);
						_logger.LogDebug($"{gameName} | {gameUrlFull}");

						var newFreeGame = new FreeGameRecord() {
							Name = gameName,
							Url = gameUrlFull,
							PossibleClaimLink = string.Format(ParseString.possibleClaimLinkFormat, RemoveSpecialCharacters(gameName))
						};

						resultList.Add(newFreeGame);

						if (oldRecords.Count == 0 || !oldRecords.Exists(record => record.Name == gameName && record.Url == gameUrlFull)) {
							pushList.Add(newFreeGame);
							_logger.LogInformation(ParseString.infoAddToList, gameName);
						} else _logger.LogInformation(ParseString.infoFoundInPreviousRecords, gameName);
					}
				} else _logger.LogInformation(ParseString.debugNoGiveawayDetected);

				_logger.LogDebug($"Done: {ParseString.debugHtmlParser}");
				return new(resultList, pushList);
			} catch (Exception) {
				_logger.LogError($"Error: {ParseString.debugHtmlParser}");
				throw;
			} finally {
				Dispose();
			}
		}

		private static string RemoveSpecialCharacters(string str) {
			if (str == null) return string.Empty;
			return Regex.Replace(str, ParseString.removeSpecialCharsRegex, string.Empty);
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
