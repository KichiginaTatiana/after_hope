using System.Threading.Tasks;

namespace AfterHope.BotService
{
    public interface IBotService
    {
        void Start();
        void Stop();
        Task Ping();
    }
}