using System;
using System.IO;
using AfterHope.BotService;
using AfterHope.Configuration;

namespace AfterHope
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Directory.Exists("data"))
                Directory.CreateDirectory("data");

            var container = ContainerConfiguration.Configure();
            var botService = container.Locate<IBotService>();
            botService.Start();
            botService.Ping().Wait();
            Console.WriteLine("Bot started. Press any key to stop it!");
            Console.ReadLine();
            botService.Stop();
            container.Dispose();
        }
    }
}
