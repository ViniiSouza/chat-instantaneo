using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;

        public async Task SendMessage(string usuarioEnvio, string mensagem, string usuarioDestino, string connectionId)
        {
            await Clients.All.SendAsync("ReceiveMessage", usuarioEnvio, mensagem, usuarioDestino, connectionId);
        }
    }
}
