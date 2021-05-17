using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.API;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace Web.Data.Data.Wrapper.SubModules
{
    public class Patch
    {
        private readonly HttpClient client;
        private readonly string defaultUrl;

        public Patch(HttpClient client, string defaultUrl)
        {
            this.client = client;
            this.defaultUrl = defaultUrl;
        }

        public async Task<bool> Send(string objectName, int id, JsonPatchDocument patch)
        {
            string requestUrl = $"api/{objectName}/{id}";

            var payload = JsonConvert.SerializeObject(patch);

            HttpContent content = new StringContent(payload, Encoding.UTF8, "application/json");

            return await GetResponseAsync(requestUrl, content);
        }

        private async Task<bool> GetResponseAsync(string requestUrl, HttpContent content)
        {
            HttpResponseMessage response = await client.PatchAsync($"{defaultUrl}{requestUrl}", content);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
