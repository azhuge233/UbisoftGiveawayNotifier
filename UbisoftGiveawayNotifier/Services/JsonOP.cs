using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UbisoftGiveawayNotifier.Models.Config;
using UbisoftGiveawayNotifier.Models.Record;
using UbisoftGiveawayNotifier.Strings;

namespace UbisoftGiveawayNotifier.Services {
	internal class JsonOP: IDisposable {
		private readonly ILogger<JsonOP> _logger;

		public JsonOP(ILogger<JsonOP> logger) {
			_logger = logger;
		}

		public void WriteData(List<FreeGameRecord> data) {
			try {
				if (data.Count > 0) {
					_logger.LogDebug(JsonOPString.debugWrite);
					string json = JsonConvert.SerializeObject(data, Formatting.Indented);
					File.WriteAllText(JsonOPString.recordsPath, string.Empty);
					File.WriteAllText(JsonOPString.recordsPath, json);
					_logger.LogDebug($"Done: {JsonOPString.debugWrite}");
				} else _logger.LogDebug("No records detected, quit writing records");
			} catch (Exception) {
				_logger.LogError($"Error: {JsonOPString.debugWrite}");
				throw;
			} finally {
				Dispose();
			}
		}

		public List<FreeGameRecord> LoadData() {
			try {
				_logger.LogDebug(JsonOPString.debugLoadRecords);
				var content = JsonConvert.DeserializeObject<List<FreeGameRecord>>(File.ReadAllText(JsonOPString.recordsPath));
				_logger.LogDebug($"Done: {JsonOPString.debugLoadRecords}");
				return content;
			} catch (Exception) {
				_logger.LogError($"Error: {JsonOPString.debugLoadRecords}");
				throw;
			}
		}

		public Config LoadConfig() {
			try {
				_logger.LogDebug(JsonOPString.debugLoadConfig);
				var content = JsonConvert.DeserializeObject<Config>(File.ReadAllText(JsonOPString.configPath)) ?? new Config();
				_logger.LogDebug($"Done: {JsonOPString.debugLoadConfig}");
				return content;
			} catch (Exception) {
				_logger.LogError($"Error: {JsonOPString.debugLoadConfig}");
				throw;
			}
		}
		public void Dispose() {
			GC.SuppressFinalize(this);
		}
	}
}
