namespace UbisoftGiveawayNotifier.Strings {
	internal class JsonOPString {
		#region path strings
		internal static readonly string configPath = $"{AppDomain.CurrentDomain.BaseDirectory}Config File{Path.DirectorySeparatorChar}config.json";
		internal static readonly string recordsPath = $"{AppDomain.CurrentDomain.BaseDirectory}Records{Path.DirectorySeparatorChar}Records.json";
		#endregion

		#region debug strings
		internal static readonly string debugWrite = "Write records";
		internal static readonly string debugLoadConfig = "Load config";
		internal static readonly string debugLoadRecords = "Load previous records";
		#endregion
	}
}
