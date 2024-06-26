using Newtonsoft.Json;

namespace UbisoftGiveawayNotifier.Models.UbisoftGraphQLJsonData {
	public class JsonData {
		[JsonProperty("data")]
		public Data Data { get; set; }
	}

	public class Data {
		[JsonProperty("promoMaster")]
		public PromoMaster PromoMaster { get; set; }
	}

	public class Sys {
		[JsonProperty("id")]
		public string ID { get; set; }
	}

	public class PromoMaster {
		[JsonProperty("sys")]
		public Sys Sys { get; set; }

		[JsonProperty("item")]
		public PromoMasterItem Item { get; set; }
	}

	public class PromoMasterItem {
		[JsonProperty("sys")]
		public Sys Sys { get; set; }

		[JsonProperty("title")]
		public string Title { get; set; }

		[JsonProperty("subtitle")]
		public string Subtitle { get; set; }

		[JsonProperty("content")]
		public string Content { get; set; }

		[JsonProperty("buttonsMaster")]
		public ButtonsMaster ButtonsMaster {  get; set; }
	}

	public class ButtonsMaster {
		[JsonProperty("items")]
		public List<ButtonsMasterItem> Items { get; set; }
	}

	public class ButtonsMasterItem {
		[JsonProperty("sys")]
		public Sys Sys { get; set; }

		[JsonProperty("localizedItems")]
		public LocalizedItems LocalizedItems { get; set; }


	}

	public class LocalizedItems {
		[JsonProperty("sys")]
		public Sys Sys { get; set; }

		[JsonProperty("buttonText")]
		public string ButtonText {  get; set; }

		[JsonProperty("buttonUrl")]
		public string ButtonUrl { get; set; }
	}
}
