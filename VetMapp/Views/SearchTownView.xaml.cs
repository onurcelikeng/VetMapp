using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VetMapp.Views
{
    public sealed partial class SearchTownView : Page
    {
        List<string> townList = new List<string>();


        public SearchTownView()
        {
            this.InitializeComponent();
            cityName.Text = SearchVetView.City;
            SelectTownList();
            listView.ItemsSource = townList;
        }


        private void SelectTownList()
        {
            townList.Clear();
            for (int i = 0; i < SearchCityView.vetList.Count; i++)
            {
                if (isExist(SearchCityView.vetList[i].Town) == false && SearchVetView.City == SearchCityView.vetList[i].City)
                {
                    townList.Add(SearchCityView.vetList[i].Town);
                }
            }

            townList.Sort();
        }

        private bool isExist(string town)
        {
            for (int i = 0; i < townList.Count; i++)
            {
                if (townList[i] == town)
                {
                    return true;
                }
            }

            return false;
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SearchVetView.Town = listView.SelectedItem as string;
            Frame.Navigate(typeof(SearchVetView));
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            string filter = sender.Text.ToUpper();
            listView.ItemsSource = townList.Where(s => (s).ToUpper().Contains(filter));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}