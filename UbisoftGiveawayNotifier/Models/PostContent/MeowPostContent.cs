using System.Text.Json.Serialization;

namespace UbisoftGiveawayNotifier.Models.PostContent {
	public class MeowPostContent {
		[JsonPropertyName("title")]
		public string Title { get; set; }
		[JsonPropertyName("msg")]
		public string Message { get; set; }
		[JsonPropertyName("url")]
		public string Url { get; set; }
	}
}