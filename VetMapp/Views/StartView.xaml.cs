using Facebook;
using Parse;
using System;
using System.Collections.Generic;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VetMapp.Views
{
    public sealed partial class StartView : Page
    {

        public StartView()
        {
            this.InitializeComponent();
            this.InitializeUI();
        }


        private void InitializeUI()
        {
            if (ApiInformation.IsApiContractPresent("Windows.Phone.PhoneContract", 1, 0))
            {
                var statusBar = StatusBar.GetForCurrentView();
                statusBar.ForegroundColor = Colors.White;
            }
        }

        private async void FacebookLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            progress.IsIndeterminate = true;

            try
            {
                var user = await ParseFacebookUtils.LogInAsync(new List<string>() { "email", "public_profile" });
                if (user != null)
                {   
                    FacebookClient client = new FacebookClient(ParseFacebookUtils.AccessToken);
                    dynamic facebookUser = await client.GetTaskAsync("me", new { fields = "id,name,email" });

                    if (!user.Keys.Contains("nameSurname"))
                        user.Add("nameSurname", facebookUser.name);
                    else
                        user["nameSurname"] = facebookUser.name;
                    if (!user.Keys.Contains("withFacebook"))
                        user.Add("withFacebook", true);
                    if (!user.Keys.Contains("email"))
                        user.Add("email", facebookUser.email);
                    else
                        user["email"] = facebookUser.email;
                    await user.SaveAsync();

                    backgroundPanel.Visibility = Visibility.Visible;
                    successfulLoginPanel.Visibility = Visibility.Visible;
                    NameSurname.Text = ParseUser.CurrentUser["nameSurname"] as string;
                }
            }

            catch (Exception)
            {
            }

            progress.IsIndeterminate = false;
        }

        private void Register_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterView));
        }

        private void Login_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginView));
        }

        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = (sender as Grid).Name;

            if (button == "closeButtonForSuccessful")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                successfulLoginPanel.Visibility = Visibility.Collapsed;

                Frame.Navigate(typeof(MainPage));
            }
        }
    }
}