using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SimpleTcp;

namespace WebClient.Services.Impl
{
    public class ClientManager : ITcpClientManager

    {
        private Dictionary<string, SimpleTcpClient> _clients = new Dictionary<string, SimpleTcpClient>();

        public string StartNew()
        {
            string id = Guid.NewGuid().ToString();

            // instantiate
            var client = new SimpleTcpClient("127.0.0.1:9000");

            _clients.Add(id, client);

            // set events
            client.Events.Connected += (sender,args) => Connected(id, sender, args);
            client.Events.Disconnected += (sender, args) => Disconnected(id, sender, args);
            client.Events.DataReceived += (sender, args) => DataReceived(id, sender, args);

            // let's go!
            client.Connect();

            return id;
        }

        public async Task Send(string id, string msg)
        {
            await _clients[id].SendAsync(msg);
        }

        private void Connected(string id, object sender, EventArgs e)
        {
            Console.WriteLine("*** Server connected");
        }

        private void Disconnected(string id, object sender, EventArgs e)
        {
            Console.WriteLine("*** Server disconnected");
        }

        private void DataReceived(string id, object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("[" + e.IpPort + "] " + Encoding.UTF8.GetString(e.Data));
        }
    }
}