using BlazorSignalRClient.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Text;

namespace BlazorSignalRClient.Pages.Article
{
    public partial class ArticleCreate
    {
        [Inject]
        public HttpClient Client { get; set; }
        public ArticleModel myform { get; set; }

        protected override void OnInitialized()
        {
            myform = new ArticleModel();
        }

        public async Task submit()
        {
            string json = JsonConvert.SerializeObject(myform);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using(HttpResponseMessage message = await Client.PostAsync("article", content))
            {
                if (!message.IsSuccessStatusCode) { Console.WriteLine(message.Content); } 
            }
        }
    }
}
