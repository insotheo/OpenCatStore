using System.Collections.Generic;

namespace OpenCatStoreAPI
{
    public class Library
    {
        public List<LibraryItem> LibraryItems { get; set; }

        public Library()
        {
            LibraryItems = new List<LibraryItem>();
        }

        public static LibraryItem MakeLibraryItem(string title, string owner, string langs, string license)
        {
            return new LibraryItem()
            {
                AppName = title,
                Author = owner,
                Languages = langs,
                License = license,
                InLibrarySince = System.DateTime.Now
            };
        }

        public bool Contains(LibraryItem item)
        {
            foreach(LibraryItem i in LibraryItems)
            {
                if(item.AppName == i.AppName &&
                    item.Author == i.Author &&
                    item.Languages == i.Languages &&
                    item.License == i.License)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
