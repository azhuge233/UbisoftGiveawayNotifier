using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal interface INotifiable: IDisposable {
		internal Task SendMessage(NotifyConfig config, List<FreeGameRecord> records);
	}
}
