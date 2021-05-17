using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Data.Wrapper.SubModules
{
    public class Delete
    {
        private readonly HttpClient client;
        private readonly string defaultUrl;

        public Delete(HttpClient client, string defaultUrl)
        {
            this.client = client;
            this.defaultUrl = defaultUrl;
        }

        public async Task<bool> Author(int id)
        {
            string requestUrl = $"api/Author/{id}";
            return await GetDeleteResponseAsync(requestUrl);
        }

        public async Task<bool> Literature(int id)
        {
            string requestUrl = $"api/Literatures/{id}";
            return await GetDeleteResponseAsync(requestUrl);
        }

        public async Task<bool> Subject(int id)
        {
            string requestUrl = $"api/Subjects/{id}";
            return await GetDeleteResponseAsync(requestUrl);
        }

        private async Task<bool> GetDeleteResponseAsync(string requestUrl)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{defaultUrl}{requestUrl}");
            return response.StatusCode == HttpStatusCode.NoContent;
        }
    }

}
