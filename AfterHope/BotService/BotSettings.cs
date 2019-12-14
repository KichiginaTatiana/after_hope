using System;

namespace AfterHope.BotService
{
    public class BotSettings : IBotSettings
    {
        public string Token => throw new NotImplementedException();

        public string ProxyHostName => "130.185.79.95";

        public int ProxyPort => 1080;

        public bool UseProxy => false;
    }
}