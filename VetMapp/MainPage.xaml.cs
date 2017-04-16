using VetMapp.Views;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VetMapp
{
    public sealed partial class MainPage : Page
    {
        private int index;


        public MainPage()
        {
            this.InitializeComponent();
            this.InitializeUI();
        }


        private void InitializeUI()
        {

            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.ForegroundColor = Colors.Black;
            }

            index = 0;
            mapOnline.Opacity = 1; mapOffline.Opacity = 0;
            searchOnline.Opacity = 0; searchOffline.Opacity = 1;
            informationOnline.Opacity = 0; informationOffline.Opacity = 1;
            profileOnline.Opacity = 0; profileOffline.Opacity = 1;

            iframe.Navigate(typeof(MapView));
        }

        private void mapButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(index != 0)
            {
                index = 0;
                mapOnline.Opacity = 1; mapOffline.Opacity = 0;
                searchOnline.Opacity = 0; searchOffline.Opacity = 1;
                informationOnline.Opacity = 0; informationOffline.Opacity = 1;
                profileOnline.Opacity = 0; profileOffline.Opacity = 1;

                iframe.Navigate(typeof(MapView));
            }
        }

        private void searchButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(index != 1)
            {
                index = 1;
                mapOnline.Opacity = 0; mapOffline.Opacity = 1;
                searchOnline.Opacity = 1; searchOffline.Opacity = 0;
                informationOnline.Opacity = 0; informationOffline.Opacity = 1;
                profileOnline.Opacity = 0; profileOffline.Opacity = 1;

                iframe.Navigate(typeof(SearchCityView));
            }
        }

        private void informationButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(index != 2)
            {
                index = 2;
                mapOnline.Opacity = 0; mapOffline.Opacity = 1;
                searchOnline.Opacity = 0; searchOffline.Opacity = 1;
                informationOnline.Opacity = 1; informationOffline.Opacity = 0;
                profileOnline.Opacity = 0; profileOffline.Opacity = 1;

                iframe.Navigate(typeof(SearchInformationView));
            }
        }

        private void profileButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(index != 3)
            {
                index = 3;
                mapOnline.Opacity = 0; mapOffline.Opacity = 1;
                searchOnline.Opacity = 0; searchOffline.Opacity = 1;
                informationOnline.Opacity = 0; informationOffline.Opacity = 1;
                profileOnline.Opacity = 1; profileOffline.Opacity = 0;

                iframe.Navigate(typeof(ProfileView));
            }
        }
    }
}