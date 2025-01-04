using OpenCatStoreAPI;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace OpenCatStoreApp.CustomControls
{
    /// <summary>
    /// Interaction logic for StoreSearchPage.xaml
    /// </summary>
    public partial class StoreSearchPage : UserControl
    {
        public StoreSearchPage()
        {
            InitializeComponent();

            BrowseBtn.Click += Browse;
            SearchQueryTB.KeyDown += SearchQueryTB_KeyDown;
        }

        private void SearchQueryTB_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                Search();
            }
        }

        private void Browse(object sender, RoutedEventArgs e) => Search();

        private void Search()
        {
            string searchQuery = SearchQueryTB.Text;
            if (string.IsNullOrWhiteSpace(searchQuery))
            {
                MessageBox.Show("Please enter a search query");
                ResultsListBox.Items.Clear();
                ItemsCounterTB.Text = "";
                return;
            }

            List<SearchResultInfo> result = Task.Run(() => GithubAPI.Search(searchQuery)).Result;

            if (result == null || result.Count == 0)
            {
                MessageBox.Show("No results found");
                ResultsListBox.Items.Clear();
                ItemsCounterTB.Text = "Items found: 0";
                return;
            }

            ResultsListBox.Items.Clear();

            ItemsCounterTB.Text = "Items found: " + result.Count.ToString();
            foreach (SearchResultInfo resultInfo in result)
            {
                ResultsListBox.Items.Add(new StoreItem(resultInfo));
            }
        }
    }
}
