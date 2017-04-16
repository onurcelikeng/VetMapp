using System;
using System.Collections.Generic;
using System.Linq;
using VetMapp.Core;
using VetMapp.Helpers;
using VetMapp.Models;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

namespace VetMapp.Views
{
    public sealed partial class VetDetailsView : Page
    {
        public static VetModel vet;


        public VetDetailsView()
        {
            this.InitializeComponent();
            this.DataContext = vet;
            this.setVetImages();
            this.setVetServices();
            this.setVetWorkingHours();
            this.setUserPets();
            this.PinToMap(vet);
        }


        private void setVetImages()
        {
            if (vet.Images != null)
            {
                foreach (Parse.ParseFile item in vet.Images)
                {
                    flipView.Items.Add(item.Url.OriginalString);
                }
            }
        }

        private void setVetServices()
        {
            var services = vet.Services;
            if (services != null)
            {
                foreach (var service in services)
                {
                    serviceTextBlock.Text += string.Concat(service, ", ");
                }
            }

            else
            {
                serviceTextBlock.Text = "Bilinmiyor";
            }
        }

        private void setVetWorkingHours()
        {
            string workingHours = "";
            if (vet.WorkingDaysAndHours != null)
            {
                IList<object> list = vet.WorkingDaysAndHours.ToList();
                Dictionary<string, string> dictionary = VetHelper.getDictionary(list[0]);

                if (dictionary["allDay"] != null)
                {
                    if (dictionary["allDay"] == "True")
                    {
                        workingHours = "7 / 24 Açık";
                    }

                    else
                    {
                        var addTime = DateTime.Parse("1.01.2000 04:00:00").ToUniversalTime().TimeOfDay;

                        if (dictionary["workingOpenHour"] != null || dictionary["workingCloseHour"] != null)
                        {
                            var openTime = DateTime.Parse(dictionary["workingOpenHour"]).ToUniversalTime().TimeOfDay + addTime;
                            var closeTime = DateTime.Parse(dictionary["workingCloseHour"]).ToUniversalTime().TimeOfDay + addTime;

                            workingHours += "Hafta içi : " + openTime.ToString(@"hh\:mm") + "'dan " + closeTime.ToString(@"hh\:mm") + "'a kadar açıktır.\n";
                        }

                        if (dictionary.ContainsKey("saturday") && dictionary["saturday"] == "True")
                        {
                            var openTime = DateTime.Parse(dictionary["saturdayOpenHour"]).ToUniversalTime().TimeOfDay + addTime;
                            var closeTime = DateTime.Parse(dictionary["saturdayCloseHour"]).ToUniversalTime().TimeOfDay + addTime;

                            workingHours += "Cumartesi : " + openTime.ToString(@"hh\:mm") + "'dan " + closeTime.ToString(@"hh\:mm") + "'a kadar açıktır.\n";
                        }

                        if (dictionary.ContainsKey("sunday") && dictionary["sunday"] == "True")
                        {
                            var openTime = DateTime.Parse(dictionary["sundayOpenHour"]).ToUniversalTime().TimeOfDay + addTime;
                            var closeTime = DateTime.Parse(dictionary["sundayCloseHour"]).ToUniversalTime().TimeOfDay + addTime;

                            workingHours += "Pazar : " + openTime.ToString(@"hh\:mm") + "'dan " + closeTime.ToString(@"hh\:mm") + "'a kadar açıktır.\n";
                        }
                    }
                }

                workingHourTextBlock.Text = workingHours;
            }

            else
            {
                workingHourTextBlock.Text = "Bilinmiyor";
            }
        }

        private async void setUserPets()
        {
            try
            {
                listView.ItemsSource = await DataClient.Instance.Pets();
            }

            catch (Exception)
            {

            }
        }

        private async void PinToMap(VetModel vet)
        {
            BasicGeoposition pos = new BasicGeoposition()
            {
                Latitude = vet.Location.Latitude,
                Longitude = vet.Location.Longitude
            };

            MapIcon icon = new MapIcon()
            {
                Visible = true,
                Location = new Geopoint(pos),
                NormalizedAnchorPoint = new Point() { X = 0.32, Y = 0.78 },
                Image = RandomAccessStreamReference.CreateFromUri(new Uri(vet.MapPin, UriKind.RelativeOrAbsolute))
            };

            await map.TrySetViewAsync(icon.Location, 13, 0, 0, MapAnimationKind.Bow);

            map.MapElements.Add(icon);
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void menuButtons_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = (sender as Rectangle).Name;

            if (button == "callButton")
            {
                backgroundPanel.Visibility = Visibility.Visible;
                callPanel.Visibility = Visibility.Visible;
            }

            else if (button == "mapButton" || button == "mapButton2")
            {
                await Launcher.LaunchUriAsync(new Uri("ms-drive-to:?destination.latitude=" + vet.Location.Latitude.ToString().Replace(",", ".") + "&destination.longitude=" + vet.Location.Longitude.ToString().Replace(",", ".") + "&destination.name=" + vet.Name));
            }

            else if (button == "mailButton")
            {
                backgroundPanel.Visibility = Visibility.Visible;
                mailPanel.Visibility = Visibility.Visible;
            }

            else if (button == "webButton")
            {
                backgroundPanel.Visibility = Visibility.Visible;
                webPanel.Visibility = Visibility.Visible;
            }

            else if (button == "anotherButton")
            {
                backgroundPanel.Visibility = Visibility.Visible;
                anotherPanel.Visibility = Visibility.Visible;
            }
        }

        private async void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                var button = (sender as Grid).Name;

                #region Call Panel

                if (button == "homeCallButton")
                {
                    Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(vet.PhoneNumber.Replace("-", ""), vet.Name);
                    backgroundPanel.Visibility = Visibility.Collapsed;
                    callPanel.Visibility = Visibility.Collapsed;
                }

                else if (button == "gsmCallButton")
                {
                    Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(vet.CellNumber.Replace("-", ""), vet.Name);
                    backgroundPanel.Visibility = Visibility.Collapsed;
                    callPanel.Visibility = Visibility.Collapsed;
                }

                else if (button == "exitButtonForCallPanel")
                {
                    backgroundPanel.Visibility = Visibility.Collapsed;
                    callPanel.Visibility = Visibility.Collapsed;
                }

                #endregion

                #region Mail Panel

                else if (button == "mailWithoutInfoButton")
                {
                    if (!string.IsNullOrEmpty(vet.Email))
                    {
                        EmailHelper helper = new EmailHelper();
                        helper.SendEmail(null, vet.Name, vet.Email);

                        backgroundPanel.Visibility = Visibility.Collapsed;
                        mailPanel.Visibility = Visibility.Collapsed;
                    }
                }

                else if (button == "exitButtonForEmailPanel")
                {
                    backgroundPanel.Visibility = Visibility.Collapsed;
                    mailPanel.Visibility = Visibility.Collapsed;
                }

                #endregion

                #region Web Panel

                else if (button == "showButton")
                {

                }

                else if (button == "exitButtonForWebPanel")
                {
                    backgroundPanel.Visibility = Visibility.Collapsed;
                    webPanel.Visibility = Visibility.Collapsed;
                }

                #endregion

                #region AnotherPanel

                else if (button == "twitterButton")
                {
                    if (!string.IsNullOrEmpty(vet.TwitterAccount)) await Launcher.LaunchUriAsync(new Uri(vet.TwitterAccount));
                }

                else if (button == "instagramButton")
                {
                    if (!string.IsNullOrEmpty(vet.InstagramAccount)) await Launcher.LaunchUriAsync(new Uri(vet.InstagramAccount));
                }

                else if (button == "facebookButton")
                {
                    if (!string.IsNullOrEmpty(vet.FacebookPage)) await Launcher.LaunchUriAsync(new Uri(vet.FacebookPage));
                }

                else if (button == "exitButtonForAnotherPanel")
                {
                    backgroundPanel.Visibility = Visibility.Collapsed;
                    anotherPanel.Visibility = Visibility.Collapsed;
                }

                #endregion
            }

            catch (Exception)
            {

            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedPet = (listView.SelectedItem) as PetModel;

            if(!string.IsNullOrEmpty(vet.Email))
            {
                EmailHelper helper = new EmailHelper();
                helper.SendEmail(selectedPet, vet.Name, vet.Email);

                backgroundPanel.Visibility = Visibility.Collapsed;
                mailPanel.Visibility = Visibility.Collapsed;
            }
        }
    }
}