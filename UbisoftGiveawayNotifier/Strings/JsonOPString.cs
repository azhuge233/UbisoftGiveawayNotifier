namespace UbisoftGiveawayNotifier.Strings {
	internal class JsonOPString {
		#region path strings
		internal static readonly string recordsPath = $"{AppDomain.CurrentDomain.BaseDirectory}Records{Path.DirectorySeparatorChar}Records.json";
		#endregion

		#region debug strings
		internal const string debugWrite = "Write records";
		internal const string debugLoadConfig = "Load config";
		internal const string debugLoadRecords = "Load previous records";
		#endregion
	}
}
