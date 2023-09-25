using Microsoft.AspNetCore.Components;
using BlazorSignalRClient.Models;
using Newtonsoft.Json;

namespace BlazorSignalRClient.Pages.Article
{
    public partial class ArticleDetail
    {
        [Inject]
        public HttpClient Client { get; set; }

        public ArticleModel CurrentArticle { get; set; }

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            
            await GetArticle();
        }

        private async Task GetArticle()
        {
            using (HttpResponseMessage message = await Client.GetAsync("article/"+ Id))
            {
                if (message.IsSuccessStatusCode)
                {
                    string json = await message.Content.ReadAsStringAsync();
                    CurrentArticle = JsonConvert.DeserializeObject<ArticleModel>(json);
                }
            }
        }


    }
}
