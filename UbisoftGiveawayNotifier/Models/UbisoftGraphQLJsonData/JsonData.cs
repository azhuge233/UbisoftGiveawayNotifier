using Newtonsoft.Json;

namespace UbisoftGiveawayNotifier.Models.UbisoftGraphQLJsonData {
	public class Data {
		[JsonProperty("promoMaster")]
		public PromoMaster PromoMaster { get; set; }
	}

	public class PromoMaster {
		[JsonProperty("sys")]
		public Sys Sys { get; set; }

		[JsonProperty("item")]
		public Item Item { get; set; }
	}

	public class Sys {
		[JsonProperty("id")]
		public string ID { get; set; }
	}

	public class Item {
		[JsonProperty("sys")]
		public Sys Sys { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("subtitle")]
		public string Subtitle { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }
	}
}
