using Parse;
using VetMapp.Core;
using VetMapp.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VetMapp.Views
{
    public sealed partial class ProfileView : Page
    {

        public ProfileView()
        {
            this.InitializeComponent();
            this.Loaded += ProfileView_Loaded;
        }


        private void ProfileView_Loaded(object sender, RoutedEventArgs e)
        {
            ParseUser user = ParseUser.CurrentUser;

            if (user != null)
            {
                if (user["nameSurname"] != null)
                {
                    nameSurname.Text = user["nameSurname"].ToString();
                }

                if (user.ContainsKey("withFacebook") == true)
                {
                    if (user["withFacebook"].ToString() == "True")
                    {
                        username.Text = "Facebook ile bağlanıldı.";
                    }

                    else
                    {
                        username.Text = user["username"].ToString();
                    }
                }

                else
                {
                    username.Text = user["username"].ToString();
                }

                if (user["email"] != null)
                {
                    email.Text = user["email"].ToString();
                }
            }

            setUserPets();
        }

        private async void setUserPets()
        {
            progress.IsIndeterminate = true;

            try
            {
                var response = await DataClient.Instance.Pets();
                listView.ItemsSource = response;
            }

            catch (System.Exception)
            {

            }

            progress.IsIndeterminate = false;
        }

        private void addPet_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PetView.isUpdate = false;
            Frame.Navigate(typeof(PetView));
        }

        private void exit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            backgroundPanel.Visibility = Visibility.Visible;
            logOutPanel.Visibility = Visibility.Visible;
        }

        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = (sender as Grid).Name;

            if (button == "acceptButton")
            {
                ParseUser.LogOut();
                Application.Current.Exit();
            }

            else if (button == "exitButtonForLogOutPanel")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                logOutPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PetView.isUpdate = true;
            PetView.pet = listView.SelectedItem as PetModel;
            Frame.Navigate(typeof(PetView));
        }
    }
}