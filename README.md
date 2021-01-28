# Telegram Logging Provider
The provider that allows you to write logs to Telegram as to in alternative source.

### Requirements: 
- Telegram bot
- Public/private channel

### Installation:
1) [Create a telegram bot](https://core.telegram.org/bots#3-how-do-i-create-a-bot) and a channel.
2) Get your channel id. To do that, you can forward any message from your channel to [@JsonDumpBot](https://www.t.me/JsonDumpBot).
3) Add a reference to the library and to [Microsoft Logging Package](https://www.nuget.org/packages/Microsoft.Extensions.Logging/). 
4) Configure logger and provier.
5) Add provider.

### Configuring a provider

A provider can be configured from code or from the configuration file.


#### From code configuring
```csharp
var options = new TelegramLoggerOptions
{
    AccessToken = "0000000000:XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
    ChatId = "-0000000000000",
    LogLevel = LogLevel.Error,
    UseEmoji = false
};
```
Access token - telegram bot token
ChatId - telegram channel id
LogLevel - minimum Level of logs
UseEmoji - change log level text representation to emoji representation 

#### From Json configuration
```json

"Logging": {
  "Telegram": {
    "AccessToken": "0000000000:XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
    "ChatId": -0000000000000,
    "LogLevel": "Error",
    "UseEmoji": "false"
  }
}

```

### Logger using
As this provider realizes the `ILogger` interface you can interact with it like with a common logger.

### Usage example

```csharp

var options = new TelegramLoggerOptions
{
    AccessToken = "0000000000:XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
    ChatId = "-0000000000000",
    LogLevel = LogLevel.Error,
    UseEmoji = false
};

var factory = LoggerFactory.Create(builder =>
{
    builder.ClearProviders()
        .AddTelegram(options);
});

var logger = factory.CreateLogger<ExampleClass>();

```

or 

```csharp
var builder = new ConfigurationBuilder();
var config = builder.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var factory = LoggerFactory.Create(builder =>
{
    builder.ClearProviders()
        .AddTelegram(config);
});
```
For more examples visit the examples\` directory


#### Log example

![Log Example](https://i.imgur.com/pBwdaqp.png)


### Limitations
- Bot will not be able to send more than 20 messages per minute.


Original idea: https://github.com/ernado-x/X.Extensions.Logging.Telegram
