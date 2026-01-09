using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.WebSockets;
using System.Text.Json;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Models.WebSocketContent;
using UbisoftGiveawayNotifier.Strings;
using Websocket.Client;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class QQWebSocket(ILogger<QQWebSocket> logger, IOptions<Config> config) : INotifiable {
		private readonly ILogger<QQWebSocket> _logger = logger;
		private readonly Config config = config.Value;

		private WebsocketClient GetWSClient() {
			var url = new Uri(string.Format(NotifyFormatString.qqWebSocketUrlFormat, config.QQWebSocketAddress, config.QQWebSocketPort, config.QQWebSocketToken));

			#region new websocket client
			var client = new WebsocketClient(url);
			client.ReconnectionHappened.Subscribe(info => _logger.LogDebug(NotifierString.debugWSReconnection, info.Type));
			client.MessageReceived.Subscribe(msg => _logger.LogDebug(NotifierString.debugWSMessageRecieved, msg));
			client.DisconnectionHappened.Subscribe(msg => _logger.LogDebug(NotifierString.debugWSDisconnected, msg));
			#endregion

			return client;
		}

		private List<WSPacket> GetSendPacket(List<FreeGameRecord> records) {
			return records.Select(record => new WSPacket {
				Action = NotifyFormatString.qqWebSocketSendAction,
				Params = new Param { 
					UserID = config.ToQQID,
					Message = $"{record.ToQQMessage()}{NotifyFormatString.projectLink}"
				}
			}).ToList();
		}

		public async Task SendMessage(List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugQQWebSocketSendMessage);

				var packets = GetSendPacket(records);

				using var client = GetWSClient();

				await client.Start();

				foreach (var packet in packets) {
					await client.SendInstant(JsonSerializer.Serialize(packet));
					await Task.Delay(600);
				}

				await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);

				_logger.LogDebug($"Done: {NotifierString.debugQQWebSocketSendMessage}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {NotifierString.debugQQWebSocketSendMessage}");
				throw;
			} finally {
				Dispose();
			}
		}

		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
