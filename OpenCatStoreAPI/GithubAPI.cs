using Newtonsoft.Json;
using System;
using System.Linq;
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
                            result.Add(new SearchResultInfo(item.Name, item.Owner.Login, item.Description, item.Language, item.License == null ? "NONE" : item.License.Name));
                        }

                    }
                }
            }

            return result;
        }

        public static async Task<List<AppRelease>> GetListOfReleases(string author, string repoName)
        {
            List<AppRelease> appReleases = new List<AppRelease>();

            using(HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.github.com/repos/{author}/{repoName}/releases"))
            {
                using(HttpResponseMessage resp = await client.SendAsync(req))
                {
                    string respContent = await resp.Content.ReadAsStringAsync();
                    List<Release> releases = JsonConvert.DeserializeObject<List<Release>>(respContent);

                    foreach(Release rel in releases)
                    {
                        appReleases.Add(new AppRelease()
                        {
                            AppName = repoName,
                            downloadLinks = rel.Assets.Select(x => x.browser_download_url).ToArray(),
                            Sizes = rel.Assets.Select(x => x.Size).ToArray(),
                            Description = rel.Body,
                            VersionName = rel.tag_name,
                            Names = rel.Assets.Select(x => x.Name).ToArray()
                        });
                    }
                }
            }

            return appReleases;
        }
    }
}
