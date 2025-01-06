using Newtonsoft.Json;
using OpenCatStoreAPI;
using System;
using System.IO;
using System.Text;
using System.Windows.Controls;

namespace OpenCatStoreApp.CustomControls
{
    /// <summary>
    /// Interaction logic for LibraryPage.xaml
    /// </summary>
    public partial class LibraryPage : UserControl
    {
        private string path = Path.Combine(Directory.GetCurrentDirectory(), "lib_list.json");
        public Library @Library;

        public LibraryPage()
        {
            if (!File.Exists(path))
            {
                using(FileStream fs = File.Create(path))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new Library(), Formatting.Indented));
                    fs.Seek(0, SeekOrigin.Begin);
                    fs.Write(buffer, 0, buffer.Length);
                }
            }
            @Library = JsonConvert.DeserializeObject<Library>(File.ReadAllText(path));

            InitializeComponent();
            UpdateView();
        }

        public void UpdateView()
        {
            LibraryListBox.Items.Clear();
            foreach(LibraryItem item in @Library.LibraryItems)
            {
                LibraryListBox.Items.Add(new LibraryElement(item));
            }
        }

        public void Remove(LibraryItem item)
        {
            @Library.LibraryItems.Remove(item);
            Save();
            UpdateView();
        }

        public void AddLibItem(LibraryItem library)
        {
            if (@Library.Contains(library))
            {
                throw new Exception($"[\"{library.AppName}\" by {library.Author}] is already in your library");
            }
            @Library.LibraryItems.Add(library);
            Save();
            UpdateView();
        }

        private void Save()
        {
            File.WriteAllText(path, String.Empty);
            using(FileStream file = File.OpenWrite(path))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@Library, Formatting.Indented));
                file.Seek(0, SeekOrigin.Begin);
                file.Write(buffer, 0, buffer.Length);
            }
        }
    }
}
