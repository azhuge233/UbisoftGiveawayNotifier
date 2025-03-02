using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.WebSockets;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Models.WebSocketContent;
using UbisoftGiveawayNotifier.Strings;
using Websocket.Client;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class QQWebSocket : INotifiable {
		private readonly ILogger<QQWebSocket> _logger;

		public QQWebSocket(ILogger<QQWebSocket> logger) {
			_logger = logger;
		}

		private WebsocketClient GetWSClient(NotifyConfig config) {
			var url = new Uri(string.Format(NotifyFormatString.qqWebSocketUrlFormat, config.QQWebSocketAddress, config.QQWebSocketPort, config.QQWebSocketToken));

			#region new websocket client
			var client = new WebsocketClient(url);
			client.ReconnectionHappened.Subscribe(info => _logger.LogDebug(NotifierString.debugWSReconnection, info.Type));
			client.MessageReceived.Subscribe(msg => _logger.LogDebug(NotifierString.debugWSMessageRecieved, msg));
			client.DisconnectionHappened.Subscribe(msg => _logger.LogDebug(NotifierString.debugWSDisconnected, msg));
			#endregion

			return client;
		}

		private static List<WSPacket> GetSendPacket(NotifyConfig config, List<FreeGameRecord> records) {
			return records.Select(record => new WSPacket {
				Action = NotifyFormatString.qqWebSocketSendAction,
				Params = new Param { 
					UserID = config.ToQQID,
					Message = $"{record.ToQQMessage()}{NotifyFormatString.projectLink}"
				}
			}).ToList();
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(NotifierString.debugQQWebSocketSendMessage);

				var packets = GetSendPacket(config, records);

				using var client = GetWSClient(config);

				await client.Start();

				foreach (var packet in packets) {
					await client.SendInstant(JsonConvert.SerializeObject(packet));
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
