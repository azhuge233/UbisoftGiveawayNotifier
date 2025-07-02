using UbisoftGiveawayNotifier.Models.Record;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal interface INotifiable: IDisposable {
		internal Task SendMessage(List<FreeGameRecord> records);
	}
}
