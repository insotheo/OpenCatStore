using System.Collections.Generic;

namespace OpenCatStoreAPI
{
    internal class SearchResult
    {
        public int TotalCount { get; set; }
        public List<Item> Items { get; set; }

        public class Item
        {
            public string Name { get; set; }
            public Owner Owner { get; set; }
            public string Description { get; set; }
            public string Language { get; set; }
            public License License { get; set; }
        }

        public class Owner
        {
            public string Login { get; set; }
        }

        public class License
        {
            public string Name { get; set; }
        }
    }
}
