using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.API;
using Newtonsoft.Json;

namespace Web.Data.Data.Wrapper.SubModules
{
    public class Fetch
    {
        private readonly HttpClient client;
        private readonly string defaultUrl;

        public Fetch(HttpClient client, string defaultUrl)
        {
            this.client = client;
            this.defaultUrl = defaultUrl;
        }

        public async Task<List<Author>> GetAuthorsAsync(bool include = false)
        {
            var requestUrl = $"api/Author?include={include}";

            var response = await GetStringAsync(requestUrl);

            var authors = JsonConvert.DeserializeObject<List<Author>>(response);

            return authors;
        }

        public async Task<Author> GetAuthorAsync(int id, bool include = false)
        {
            var requestUrl = $"api/Author/{id}?include={include}";

            var response = await GetStringAsync(requestUrl);

            var author = JsonConvert.DeserializeObject<Author>(response);

            return author;
        }

        public async Task<List<Literature>> GetLiteraturesAsync(bool include = false)
        {
            var requestUrl = $"api/Literatures?include={include}";

            var response = await GetStringAsync(requestUrl);

            var literatures = JsonConvert.DeserializeObject<List<Literature>>(response);

            return literatures;
        }

        public async Task<Literature> GetLiteratureAsync(int id, bool include = false)
        {
            var requestUrl = $"api/Literatures/{id}?include={include}";

            var response = await GetStringAsync(requestUrl);

            var literature = JsonConvert.DeserializeObject<Literature>(response);

            return literature;
        }

        public async Task<List<Subject>> GetSubjectsAsync(bool include = false)
        {
            var requestUrl = $"api/Subjects?include={include}";

            var response = await GetStringAsync(requestUrl);

            var subjects = JsonConvert.DeserializeObject<List<Subject>>(response);

            return subjects;
        }

        public async Task<Subject> GetSubjectAsync(int id, bool include = false)
        {
            var requestUrl = $"api/Subjects/{id}?include={include}";

            var response = await GetStringAsync(requestUrl);

            var subject = JsonConvert.DeserializeObject<Subject>(response);

            return subject;
        }
        private async Task<string> GetStringAsync(string requestUrl)
        {
            return await client.GetStringAsync($"{defaultUrl}{requestUrl}");
        }
    }
}
