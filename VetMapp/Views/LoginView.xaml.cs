using System;
using VetMapp.Core;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VetMapp.Views
{
    public sealed partial class LoginView : Page
    {

        public LoginView()
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
        }

        private async void login_Tapped(object sender, TappedRoutedEventArgs e)
        {
            progress.IsIndeterminate = true;

            try
            {
                if(!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Password))
                {
                    var response = await DataClient.Instance.Login(username.Text, password.Password);

                    if (response.IsSuccess == true)
                    {
                        backgroundPanel.Visibility = Visibility.Visible;
                        successfulLoginPanel.Visibility = Visibility.Visible;

                        NameSurname.Text = Parse.ParseUser.CurrentUser["nameSurname"] as string;
                    }

                    else
                    {
                        backgroundPanel.Visibility = Visibility.Visible;
                        unsuccessfulLoginPanel.Visibility = Visibility.Visible;
                    }
                }

                else
                {
                    backgroundPanel.Visibility = Visibility.Visible;
                    unsuccessfulLoginPanel.Visibility = Visibility.Visible;
                }
            }

            catch (Exception)
            {
                backgroundPanel.Visibility = Visibility.Visible;
                unsuccessfulLoginPanel.Visibility = Visibility.Visible;
            }

            progress.IsIndeterminate = false;
        }

        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = (sender as Grid).Name;

            if(button == "exitButton")
            {
                Frame.GoBack();
            }

            if (button == "closeButtonForSuccessful")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                successfulLoginPanel.Visibility = Visibility.Collapsed;

                Frame.Navigate(typeof(MainPage));
            }

            if (button == "closeButtonForUnSuccessful")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                unsuccessfulLoginPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}