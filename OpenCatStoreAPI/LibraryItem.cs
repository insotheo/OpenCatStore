using System;

namespace OpenCatStoreAPI
{
    public class LibraryItem
    {
        public string AppName {  get; set; }
        public string Author { get; set; }
        public string License { get; set; }
        public DateTime InLibrarySince { get; set; }
        public string Languages { get; set; }
    }
}
