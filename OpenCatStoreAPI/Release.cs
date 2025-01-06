using System.Collections.Generic;

namespace OpenCatStoreAPI
{
    internal class Release
    {
        public int id {  get; set; }
        public List<Asset> Assets {  get; set; }
        public string Body { get; set; }
        public string tag_name { get; set; }

        internal class Asset
        {
            public string Name { get; set; }
            public int Size { get; set; }
            public string browser_download_url { get; set; }
        }
    }
}
