using Serilog;
using UserContacts.Server.SerilogSettings;

namespace UserContacts.Server.Configurations;

public static class SelilogConfiguration
{
    public static void ConfigureSerilog(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;

        var telegramBotToken = config["Serilog:WriteTo:2:Args:telegramApiKey"];
        var telegramChatId = config["Serilog:WriteTo:2:Args:telegramChatId"];

        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .WriteTo.Telegram(telegramBotToken, telegramChatId)
            .CreateLogger();

        builder.Logging.AddSerilog();
    }
}
