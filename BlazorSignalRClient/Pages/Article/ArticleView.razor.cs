using BlazorSignalRClient.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Microsoft.AspNetCore.SignalR.Client;

namespace BlazorSignalRClient.Pages.Article
{
    public partial class ArticleView
    {
        [Inject]
        public HttpClient Client { get; set; }

        public HubConnection hubConnection { get; set; }

        public List<ArticleModel> MaListe { get; set; }

        public int SelectedId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            MaListe = new List<ArticleModel>();
            await GetArticle();

            hubConnection = new HubConnectionBuilder().WithUrl(new Uri("https://localhost:7135/articlehub")).Build();

            hubConnection.On("notifyNewArticle", async () => {
                await GetArticle();
                StateHasChanged();
            });

            await hubConnection.StartAsync(); 

        }

        private void ClickInfo(int id)
        {
            SelectedId = id;
        }

        private async Task GetArticle()
        {
            using(HttpResponseMessage message = await Client.GetAsync("article"))
            {
                if(message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync();
                    MaListe = JsonConvert.DeserializeObject<List<ArticleModel>>(json);
                }
            }
        }
    }
}
