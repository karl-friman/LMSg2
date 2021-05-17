using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.API;
using Newtonsoft.Json;
using Web.Data.Data.Wrapper.SubModules;

namespace Web.Data.Data.Wrapper
{
    public class APIClient : IAPIClient
    {
        private readonly HttpClient client;
        private readonly string defaultUrl;

        private readonly Delete delete;
        private readonly Fetch fetch;
        private readonly Patch patch;
        private readonly Post post;

        public APIClient(int port)
        {
            defaultUrl = $"https://localhost:{port}/";

            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("User-Agent", "Web");

            delete = new Delete(client, defaultUrl);
            fetch = new Fetch(client, defaultUrl);
            patch = new Patch(client, defaultUrl);
            post = new Post(client, defaultUrl);
        }

        public Delete Delete()
        {
            return delete;
        }

        public Fetch Fetch()
        {
            return fetch;
        }

        public Patch Patch()
        {
            return patch;
        }

        public Post Post()
        {
            return post;
        }

    }
}
