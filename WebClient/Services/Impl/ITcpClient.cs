using System.Threading.Tasks;

namespace WebClient.Services.Impl
{
    public interface ITcpClientManager
    {
        string StartNew();

        Task Send(string id, string msg);
    }
}