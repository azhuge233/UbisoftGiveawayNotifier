# UbisoftGiveawayNotifier
A CLI tool fetches free games info from Ubisoft giveaway page, sends notifications through Telegram, Email, Bark, QQ, PushPlus and DingTalk.

Currently I'm not able to find a workable source to get Ubisoft giveaways which only available in certain regions (like the Splinter's cell Double Agent giveaway is Japan only), so the program will not send notifications for any region-specific giveaways.

If you have any idea how to get the source, tell me by creating issues.

Demo Telegram Channel [@azhuge233_FreeGames](https://t.me/azhuge233_FreeGames)

## Build

Install dotnet 6.0 SDK first, you can find installation packages/guides [here](https://dotnet.microsoft.com/download).

### Publish

```
dotnet publish -c Release -o /your/path/here -r [win10-x64/osx-x64/linux-x64] --sc
```

## Usage

Fill your Telegram Bot token and chat ID in config.json

```json
{
	"TelegramToken": "xxxxxx:xxxxxx",
	"TelegramChatID": "xxxxxxxx",
}
```

Check [wiki](https://github.com/azhuge233/SteamDB-FreeGames-dotnet/wiki/Config-Description) for more notification method descriptions, note that `NotifyKeepGamesOnly` and any ASF related variables are not working for this project.

### Repeatedly running

The program will not add while/for loop, it's a scraper. To schedule the program, use cron.d in Linux(macOS) or Task Scheduler in Windows.

Tested on Windows Server 2019/2022.

## My Free Games Collection

- SteamDB
    - [https://github.com/azhuge233/SteamDB-FreeGames](https://github.com/azhuge233/SteamDB-FreeGames)(Archived)
    - [https://github.com/azhuge233/SteamDB-FreeGames-dotnet](https://github.com/azhuge233/SteamDB-FreeGames-dotnet)
- EpicBundle
    - [https://github.com/azhuge233/EpicBundle-FreeGames](https://github.com/azhuge233/EpicBundle-FreeGames)(Archived)
    - [https://github.com/azhuge233/EpicBundle-FreeGames-dotnet](https://github.com/azhuge233/EpicBundle-FreeGames-dotnet)
- Indiegala
    - [https://github.com/azhuge233/IndiegalaFreebieNotifier](https://github.com/azhuge233/IndiegalaFreebieNotifier)
- GOG
    - [https://github.com/azhuge233/GOGGiveawayNotifier](https://github.com/azhuge233/GOGGiveawayNotifier)
- Ubisoft
    - [https://github.com/azhuge233/UbisoftGiveawayNotifier](https://github.com/azhuge233/UbisoftGiveawayNotifier)
