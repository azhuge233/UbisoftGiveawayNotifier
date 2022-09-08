using Newtonsoft.Json;

namespace UbisoftGiveawayNotifier.Models.PostContent
{
    internal class Content
    {
		[JsonProperty("content")]
		public string Content_ { get; set; }
	}
    internal class DingTalkPostContent
    {
		[JsonProperty("msgtype")]
		public string MessageType { get; set; } = "text";
		[JsonProperty("text")]
		public Content Text { get; set; } = new Content();
	}
}
