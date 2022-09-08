using Microsoft.Extensions.DependencyInjection;
using NLog;
using UbisoftGiveawayNotifier.Modules;
using UbisoftGiveawayNotifier.Services;

namespace UbisoftGiveawayNotifier {
    class Program {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        static async Task Main() {
            try {
                var servicesProvider = DI.BuildDiAll();

                logger.Info(" - Start Job -");

                using (servicesProvider as IDisposable) {
                    var jsonOp = servicesProvider.GetRequiredService<JsonOP>();

                    var config = jsonOp.LoadConfig();
                    var oldRecord = jsonOp.LoadData();
                    servicesProvider.GetRequiredService<ConfigValidator>().CheckValid(config);

                    // Get page source
                    var source = await servicesProvider.GetRequiredService<Scraper>().GetUbisoftSource(config);
                    //var source = File.ReadAllText("test.html");

                    // Parse page source
                    var parseResult = servicesProvider.GetRequiredService<Parser>().Parse(source, oldRecord);

                    // Notify first, then write records
                    await servicesProvider.GetRequiredService<NotifyOP>().Notify(config, parseResult.Item2);

                    // Write new records
                    jsonOp.WriteData(parseResult.Item1);
                }

                logger.Info(" - Job End -\n");
            } catch (Exception ex) {
                logger.Error(ex.Message);
                if (ex.InnerException != null) logger.Error(ex.InnerException.Message);
            } finally {
                LogManager.Shutdown();
            }
        }
    }
}