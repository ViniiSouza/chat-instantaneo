using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Hubs
{
    public class ChatHub : Hub
    {
        public string GetConnectionId() => Context.ConnectionId;

        /// <summary>
        /// Invoked when a user send a message. Invoke a "ReceiveMessage" that send all the informations to reproduce it
        /// </summary>
        /// <param name="usuarioEnvio">Sender</param>
        /// <param name="mensagem">Message content</param>
        /// <param name="usuarioDestino">Receiver</param>
        /// <param name="time">Hours and minutes the message was sent</param>
        public async Task SendMessage(string usuarioEnvio, string mensagem, string usuarioDestino, string time)
        {
            var data = DateTime.Now;
            await Clients.All.SendAsync("ReceiveMessage", usuarioEnvio, mensagem, usuarioDestino, time);
        }
    }
}
