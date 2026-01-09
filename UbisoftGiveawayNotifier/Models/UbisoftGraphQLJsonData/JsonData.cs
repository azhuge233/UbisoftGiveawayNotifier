using System.Text.Json.Serialization;

namespace UbisoftGiveawayNotifier.Models.UbisoftGraphQLJsonData {
	public class JsonData {
		[JsonPropertyName("data")]
		public Data Data { get; set; }
	}

	public class Data {
		[JsonPropertyName("promoMaster")]
		public PromoMaster PromoMaster { get; set; }
	}

	public class Sys {
		[JsonPropertyName("id")]
		public string ID { get; set; }
	}

	public class PromoMaster {
		[JsonPropertyName("sys")]
		public Sys Sys { get; set; }

		[JsonPropertyName("item")]
		public PromoMasterItem Item { get; set; }
	}

	public class PromoMasterItem {
		[JsonPropertyName("sys")]
		public Sys Sys { get; set; }

		[JsonPropertyName("title")]
		public string Title { get; set; }

		[JsonPropertyName("subtitle")]
		public string Subtitle { get; set; }

		[JsonPropertyName("content")]
		public string Content { get; set; }

		[JsonPropertyName("buttonsMaster")]
		public ButtonsMaster ButtonsMaster {  get; set; }
	}

	public class ButtonsMaster {
		[JsonPropertyName("items")]
		public List<ButtonsMasterItem> Items { get; set; }
	}

	public class ButtonsMasterItem {
		[JsonPropertyName("sys")]
		public Sys Sys { get; set; }

		[JsonPropertyName("localizedItems")]
		public LocalizedItems LocalizedItems { get; set; }


	}

	public class LocalizedItems {
		[JsonPropertyName("sys")]
		public Sys Sys { get; set; }

		[JsonPropertyName("buttonText")]
		public string ButtonText {  get; set; }

		[JsonPropertyName("buttonUrl")]
		public string ButtonUrl { get; set; }
	}
}
