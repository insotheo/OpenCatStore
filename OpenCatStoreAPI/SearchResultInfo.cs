namespace OpenCatStoreAPI
{
    public sealed class SearchResultInfo
    {
        public string ProjectName;
        public string Author;
        public string Description;
        public string Lanugages;

        public SearchResultInfo(string projectName, string author, string description, string languages)
        {
            ProjectName = projectName;
            Author = author;
            Description = description;
            Lanugages = languages;
        }
    }
}
