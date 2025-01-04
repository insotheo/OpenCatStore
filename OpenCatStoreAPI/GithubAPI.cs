using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OpenCatStoreAPI
{
    public static class GithubAPI
    {
        private static bool isInited;
        private static HttpClient client;

        internal static void UpdateToken()
        {
            if (isInited)
            {
                client = new HttpClient();
                client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("OpenCatStore", "0.0.0.1"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", StoreConfig.GithubAPIKey);
            }
        }

        public static void Init()
        {
            if (isInited)
            {
                return;
            }
            if (StoreConfig.GithubAPIKey == String.Empty)
            {
                throw new Exception("API key is empty!");
            }

            client = new HttpClient();
            client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("OpenCatStore", "0.0.0.1"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", StoreConfig.GithubAPIKey);

            isInited = true;
        }

        public static async Task<List<SearchResultInfo>> Search(string query)
        {
            List<SearchResultInfo> result = new List<SearchResultInfo>();

            using (HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.github.com/search/repositories?q={query}"))
            {

                using (HttpResponseMessage resp = await client.SendAsync(req))
                {
                    if (resp.IsSuccessStatusCode)
                    {
                        string respContent = await resp.Content.ReadAsStringAsync();
                        SearchResult search = JsonConvert.DeserializeObject<SearchResult>(respContent);

                        foreach(SearchResult.Item item in search.Items)
                        {
                            result.Add(new SearchResultInfo(item.Name, item.Owner.Login, item.Description, item.Language));
                        }

                    }
                }
            }

            return result;
        }
    }
}
