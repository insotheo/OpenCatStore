using System.IO;
using System.IO.Compression;
using System.Net;
using System;

namespace OpenCatStoreAPI
{
    public class AppRelease
    {
        internal string[] downloadLinks { get; set; }

        public string AppName { get; set; }
        public string[] Names { get; set; }
        public string VersionName { get; set; }
        public string Description { get; set; }
        public int[] Sizes { get; set; }

        public void Download(int index)
        {
            string path = Path.Combine(StoreConfig.SaveAppsDirectory, $"{AppName}_{VersionName}");
            if (Directory.Exists(path))
            {
                throw new Exception($"Directory \"{path}\" is already exsists!");
            }

            Directory.CreateDirectory(path);

            using(WebClient client = new WebClient())
            {
                 client.DownloadFile(downloadLinks[index], Path.Combine(path, Names[index]));
            }

            if (Path.GetExtension(Names[index]).ToLower().Trim() == ".zip")
            {
                if (StoreConfig.AutomaticallyUnpackZips)
                {
                    ZipFile.ExtractToDirectory(Path.Combine(path, Names[index]), path);
                    File.Delete(Path.Combine(path, Names[index]));
                }
            }
        }
    }
}
