using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.API;
using Newtonsoft.Json;

namespace Web.Data.Data.Wrapper.SubModules
{
    public class Post
    {
        private readonly HttpClient client;
        private readonly string defaultUrl;

        public Post(HttpClient client, string defaultUrl)
        {
            this.client = client;
            this.defaultUrl = defaultUrl;
        }

        public async Task<bool> Author(Author author)
        {
            string requestUrl = $"api/Author";

            var payload = JsonConvert.SerializeObject(author);

            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            return await GetResponseAsync(requestUrl, content);
        }

        public async Task<bool> Literature(Literature literature)
        {
            string requestUrl = $"api/Literatures";

            var payload = JsonConvert.SerializeObject(literature);

            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            return await GetResponseAsync(requestUrl, content);
        }

        public async Task<bool> Subject(Subject subject)
        {
            string requestUrl = $"api/Subjects";

            var payload = JsonConvert.SerializeObject(subject);

            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            return await GetResponseAsync(requestUrl, content);
        }

        private async Task<bool> GetResponseAsync(string requestUrl, HttpContent content)
        {
            HttpResponseMessage response = await client.PostAsync($"{defaultUrl}{requestUrl}", content);
            return response.StatusCode == HttpStatusCode.Created;
        }
    }
}
