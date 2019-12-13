namespace AfterHope.BotService
{
    public interface IBotSettings
    {
        string Token { get; }

        string ProxyHostName { get; }

        int ProxyPort { get; }

        bool UseProxy { get; }
    }
}