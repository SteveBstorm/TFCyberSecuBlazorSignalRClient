

using BlazorSignalRClient.Models;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorSignalRClient.Pages
{
    public partial class Chat
    {
        public List<Message> ListeMessages { get; set; }

        public string newMessage { get; set; }
        public string Author { get; set; }
        public HubConnection hubConnection { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ListeMessages = new List<Message>();
            hubConnection = new HubConnectionBuilder().WithUrl(new Uri("https://localhost:7135/chat")).Build();

            await hubConnection.StartAsync();

            hubConnection.On("receiveMessage", 
                (Message message) => {
                    ListeMessages.Add(message);
                    StateHasChanged();
                });
        }

        public async Task EnvoiMessage()
        {
            await hubConnection.SendAsync("SendMessage",
                new Message { Author = Author, Content = newMessage }
                );
        }

        public async Task RejoindreGroup()
        {
            await hubConnection.SendAsync("JoinGroup", "bidule", Author);
            hubConnection.On("messageFromGroup",
                (Message message) => {
                    ListeMessages.Add(message);
                    StateHasChanged();
                });
        }

        public async Task SendToGroup()
        {
            await hubConnection.SendAsync("SendToGroup",
                new Message { Author = Author, Content = newMessage },
                "bidule");
        }
    }
}
