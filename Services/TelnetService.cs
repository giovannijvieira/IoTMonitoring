using System;
using System.Threading.Tasks;
using Renci.SshNet;

namespace IoTMonitoring.Services
{
    public class TelnetService
    {
        public async Task<string> SendCommandAsync(string hostname, int port, string command)
        {
            using (var client = new SshClient(hostname, port, "username", "password"))
            {
                client.Connect();
                var cmd = client.CreateCommand(command);
                var result = await Task.Run(() => cmd.Execute());
                client.Disconnect();
                return result;
            }
        }
    }
}
