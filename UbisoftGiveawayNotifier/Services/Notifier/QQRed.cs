using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.WebSockets;
using System.Text;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Models.WebSocketContent;
using UbisoftGiveawayNotifier.Strings;
using Websocket.Client;

namespace UbisoftGiveawayNotifier.Services.Notifier {
	internal class QQRed : INotifiable {
		private readonly ILogger<QQRed> _logger;

		#region debug strings
		private readonly string debugSendMessage = "Send notifications to QQ Red (Chronocat)";
		private readonly string debugWSReconnection = "Reconnection happened, type: {0}";
		private readonly string debugWSMessageRecieved = "Message received: {0}";
		private readonly string debugWSDisconnected = "Disconnected: {0}";

		#endregion

		public QQRed(ILogger<QQRed> logger) {
			_logger = logger;
		}

		private WebsocketClient GetWSClient(NotifyConfig config) {
			var url = new Uri(new StringBuilder().AppendFormat(NotifyFormatString.qqRedUrlFormat, config.RedAddress, config.RedPort).ToString());

			#region new websocket client
			var client = new WebsocketClient(url);
			client.ReconnectionHappened.Subscribe(info => _logger.LogDebug(debugWSReconnection, info.Type));
			client.MessageReceived.Subscribe(msg => _logger.LogDebug(debugWSMessageRecieved, msg));
			client.DisconnectionHappened.Subscribe(msg => _logger.LogDebug(debugWSDisconnected, msg));
			#endregion

			return client;
		}

		private static WSPacket GetConnectPacket(NotifyConfig config) {
			return new WSPacket() {
				Type = NotifyFormatString.qqRedWSConnectPacketType,
				Payload = new ConnectPayload() {
					Token = config.RedToken
				}
			};
		}

		private static List<WSPacket> GetSendPacket(NotifyConfig config, List<FreeGameRecord> records) {
			return records.Select(record => new WSPacket() {
				Type = NotifyFormatString.qqRedWSSendPacketType,
				Payload = new MessagePayload() {
					Peer = new Peer() {
						ChatType = 1,
						PeerUin = config.ToQQID
					},
					Elements = new List<object>() {
						new TextElementRoot() {
							TextElement = new TextElement() {
								Content = new StringBuilder().Append(record.ToQQMessage())
															.Append(NotifyFormatString.projectLink)
															.ToString()
							}
						}
					}
				}
			}).ToList();
		}

		public async Task SendMessage(NotifyConfig config, List<FreeGameRecord> records) {
			try {
				_logger.LogDebug(debugSendMessage);

				var packets = GetSendPacket(config, records);

				using var client = GetWSClient(config);

				await client.Start();

				await client.SendInstant(JsonConvert.SerializeObject(GetConnectPacket(config)));

				foreach (var packet in packets) {
					await client.SendInstant(JsonConvert.SerializeObject(packet));
					await Task.Delay(600);
				}

				await client.Stop(WebSocketCloseStatus.NormalClosure, string.Empty);

				_logger.LogDebug($"Done: {debugSendMessage}");
			} catch (Exception) {
				_logger.LogDebug($"Error: {debugSendMessage}");
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
