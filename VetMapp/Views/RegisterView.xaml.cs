using System;
using VetMapp.Core;
using VetMapp.Models;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VetMapp.Views
{
    public sealed partial class RegisterView : Page
    {

        public RegisterView()
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

        private async void register_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(username.Text) && !string.IsNullOrEmpty(password.Password) && !string.IsNullOrEmpty(nameSurname.Text) && !string.IsNullOrEmpty(email.Text))
            {
                var model = new RegisterModel()
                {
                    Username = username.Text,
                    Password = password.Password,
                    nameSurname = nameSurname.Text,
                    Email = email.Text,
                    phoneNumber = phoneNumber.Text,
                    isVet = false,
                    withFacebook = false,
                };

                var response = await DataClient.Instance.Register(model);

                if (response.IsSuccess == true)
                {
                    backgroundPanel.Visibility = Visibility.Visible;
                    successfulRegisterPanel.Visibility = Visibility.Visible;
                }

                else
                {
                    backgroundPanel.Visibility = Visibility.Visible;
                    unsuccessfulRegisterPanel.DataContext = response.Message;
                    unsuccessfulRegisterPanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void exit_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = (sender as Grid).Name;

            if (button == "exitButton")
            {
                Frame.GoBack();
            }

            if (button == "closeButtonForSuccessful")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                successfulRegisterPanel.Visibility = Visibility.Collapsed;

                Frame.Navigate(typeof(MainPage));
            }

            if (button == "closeButtonForUnSuccessful")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                unsuccessfulRegisterPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}