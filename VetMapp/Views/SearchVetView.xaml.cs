using System;
using System.Collections.Generic;
using VetMapp.Models;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace VetMapp.Views
{
    public sealed partial class SearchVetView : Page
    {
        private List<VetModel> vetList = new List<VetModel>();
        public static string City;
        public static string Town;


        public SearchVetView()
        {
            this.InitializeComponent();

            townName.Text = Town;
            GetVets();
        }


        private void GetVets()
        {

            foreach(var vet in SearchCityView.vetList)
            {
                if(City == vet.City && Town == vet.Town && vet.Name != "Lorem")
                {
                    vetList.Add(vet);
                }
            }

            listView.ItemsSource = vetList;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedVet = (listView.SelectedItem as VetModel);

            if(selectedVet.Color != "gray")
            {
                VetDetailsView.vet = selectedVet;
                Frame.Navigate(typeof(VetDetailsView));
            }

            else
            {
                await Launcher.LaunchUriAsync(new Uri("ms-drive-to:?destination.latitude=" + selectedVet.Location.Latitude.ToString().Replace(".", ",") + "&destination.longitude=" + selectedVet.Location.Longitude.ToString().Replace(".", ",") + "&destination.name=" + selectedVet.Name));
            }
        }
    }
}