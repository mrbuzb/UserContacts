using Serilog.Core;
using Serilog.Events;

namespace UserContacts.Server.SerilogSettings;

public class TelegramSink : ILogEventSink
{
    private readonly string _botToken;
    private readonly string _chatId;
    private readonly HttpClient _httpClient;

    public TelegramSink(string telegramApiKey, string telegramChatId)
    {
        _botToken = telegramApiKey;
        _chatId = telegramChatId;
        _httpClient = new HttpClient();
    }

    public void Emit(LogEvent logEvent)
    {
        var message = $"[{logEvent.Level}] {logEvent.RenderMessage()}";
        SendMessageAsync(message).GetAwaiter().GetResult();
    }

    private async Task SendMessageAsync(string message)
    {
        var url = $"https://api.telegram.org/bot{_botToken}/sendMessage";
        var content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "chat_id", _chatId },
            { "text", message }
        });

        await _httpClient.PostAsync(url, content);
    }
}
