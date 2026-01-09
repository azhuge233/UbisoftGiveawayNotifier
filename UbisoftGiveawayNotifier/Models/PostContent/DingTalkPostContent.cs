using System.Text.Json.Serialization;

namespace UbisoftGiveawayNotifier.Models.PostContent
{
    internal class Content
    {
		[JsonPropertyName("content")]
		public string Content_ { get; set; }
	}
    internal class DingTalkPostContent
    {
		[JsonPropertyName("msgtype")]
		public string MessageType { get; set; } = "text";
		[JsonPropertyName("text")]
		public Content Text { get; set; } = new Content();
	}
}
