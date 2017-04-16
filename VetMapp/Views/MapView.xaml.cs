using Parse;
using System;
using System.Collections.Generic;
using System.Linq;
using VetMapp.Core;
using VetMapp.Models;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Input;

namespace VetMapp.Views
{
    public sealed partial class MapView : Page
    {
        private static List<VetModel> vets = new List<VetModel>();
        private static Geopoint currentLocation;
        private int limit = 250;


        public MapView()
        {
            this.InitializeComponent();
            this.Loaded += MapView_Loaded;
        }


        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapElements.Clear();

            if (currentLocation == null)
            {
                GetLocation();
            }

            else
            {
                BasicGeoposition position = new BasicGeoposition()
                {
                    Latitude = currentLocation.Position.Latitude,
                    Longitude = currentLocation.Position.Longitude
                };
                var pin = new MapIcon()
                {
                    Location = new Geopoint(position),
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/MapPins/me.png")),
                    NormalizedAnchorPoint = new Point() { X = 0.32, Y = 0.78 }
                };

                map.MapElements.Add(pin);
                await map.TrySetViewAsync(new Geopoint(position), 10, 0, 0, MapAnimationKind.Bow);

                GetVetsRequest();
            }
        }

        private async void GetLocation()
        {
            try
            {
                var gl = new Geolocator() { DesiredAccuracy = PositionAccuracy.High };
                var location = await gl.GetGeopositionAsync(TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5));
                currentLocation = location.Coordinate.Point;

                var pin = new MapIcon()
                {
                    Location = location.Coordinate.Point,
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/MapPins/me.png")),
                    Title = "",
                    NormalizedAnchorPoint = new Windows.Foundation.Point() { X = 0.32, Y = 0.78 }
                };

                map.MapElements.Add(pin);
                await map.TrySetViewAsync(location.Coordinate.Point, 10, 0, 0, MapAnimationKind.Bow);

                GetVetsRequest();
            }

            catch (Exception)
            {

            }
        }

        private async void GetVetsRequest()
        {
            progress.IsIndeterminate = true;

            try
            {
                ParseGeoPoint geo = new ParseGeoPoint()
                {
                    Latitude = currentLocation.Position.Latitude,
                    Longitude = currentLocation.Position.Longitude
                };

                vets = await  DataClient.Instance.Vets(geo, limit);

                foreach (var vet in vets)
                {
                    PinToMap(vet);
                }
            }

            catch (Exception)
            {

            }

            progress.IsIndeterminate = false;
        }

        private void PinToMap(VetModel vet)
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

            map.MapElements.Add(icon);
        }

        #region Click Events

        private void infoButton_Click(object sender, RoutedEventArgs e)
        {
            backgroundPanel.Visibility = Visibility.Visible;
            infoPanel.Visibility = Visibility.Visible;
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            backgroundPanel.Visibility = Visibility.Visible;
            settingsPanel.Visibility = Visibility.Visible;
        }

        private async void map_MapElementClick(MapControl sender, MapElementClickEventArgs args)
        {
            MapIcon icon = args.MapElements.FirstOrDefault(x => x is MapIcon) as MapIcon;

            foreach (var vet in vets)
            {
                if (vet.Location.Latitude == icon.Location.Position.Latitude && vet.Location.Longitude == icon.Location.Position.Longitude)
                {
                    BasicGeoposition bgp = new BasicGeoposition()
                    {
                        Latitude = vet.Location.Latitude,
                        Longitude = vet.Location.Longitude
                    };

                    vetPanel.DataContext = vet;
                    vetPanel.Visibility = Visibility.Visible;
                    if(vet.Color == "gray") callButton.Visibility = Visibility.Collapsed;

                    var center = new Geopoint(bgp);
                    await map.TrySetViewAsync(center, 10, 0, 0, MapAnimationKind.Bow);

                    VetDetailsView.vet = vet;

                    break;
                }
            }
        }

        #endregion

        #region Tapped Events

        private async void mapCenterButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            vetPanel.Visibility = Visibility.Collapsed;

            BasicGeoposition position = new BasicGeoposition()
            {
                Latitude = currentLocation.Position.Latitude,
                Longitude = currentLocation.Position.Longitude
            };

            await map.TrySetViewAsync(new Geopoint(position), 10, 0, 0, MapAnimationKind.Bow);
        }

        private async void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = (sender as Grid).Name;

            if (button == "webButton")
            {
                await Launcher.LaunchUriAsync(new Uri("http://www.vetmapp.com/tr/"));
            }

            else if (button == "emailButton")
            {
                await Launcher.LaunchUriAsync(new Uri("mailto:posta@onurcelikeng.com"));
            }

            else if (button == "socialMediaButton")
            {
                backgroundPanel.Visibility = Visibility.Visible;
                infoPanel.Visibility = Visibility.Collapsed;
                socialMediaPanel.Visibility = Visibility.Visible;
            }

            else if (button == "exitButtonForInfoPanel")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                infoPanel.Visibility = Visibility.Collapsed;
            }

            else if (button == "facebookButton")
            {
                await Launcher.LaunchUriAsync(new Uri("https://www.facebook.com/vetmapp/"));
            }

            else if (button == "instagramButton")
            {
                await Launcher.LaunchUriAsync(new Uri("https://www.instagram.com/vetmapp/?hl=tr"));
            }

            else if (button == "twitterButton")
            {
                await Launcher.LaunchUriAsync(new Uri("https://twitter.com/VetMapp"));
            }

            else if (button == "exitButtonForSocialMediaPanel")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                socialMediaPanel.Visibility = Visibility.Collapsed;
            }

            else if(button == "setting1Button")
            {
                map.MapElements.Clear();

                #region Current Location

                BasicGeoposition position = new BasicGeoposition()
                {
                    Latitude = currentLocation.Position.Latitude,
                    Longitude = currentLocation.Position.Longitude
                };
                var pin = new MapIcon()
                {
                    Location = new Geopoint(position),
                    Image = RandomAccessStreamReference.CreateFromUri(new Uri("ms-appx:///Assets/MapPins/me.png")),
                    NormalizedAnchorPoint = new Point() { X = 0.32, Y = 0.78 }
                };

                map.MapElements.Add(pin);
                await map.TrySetViewAsync(new Geopoint(position), 10, 0, 0, MapAnimationKind.Bow);

                #endregion

                if (setting1Title.Text == "Şuan Açık")
                {
                    setting1Title.Text = "Tüm";
                    foreach (var vet in vets)
                    {
                        if(vet.Color == "green") PinToMap(vet);
                    }
                }

                else
                {
                    setting1Title.Text = "Şuan Açık";
                    foreach (var vet in vets)
                    {
                        PinToMap(vet);
                    }
                }

                backgroundPanel.Visibility = Visibility.Collapsed;
                settingsPanel.Visibility = Visibility.Collapsed;
            }

            else if(button == "setting2Button")
            {
                if(setting2Title.Text == "Hybird")
                {
                    setting2Title.Text = "Standart";
                    map.Style = MapStyle.Aerial;
                }

                else
                {
                    setting2Title.Text = "Hybird";
                    map.Style = MapStyle.Road;
                }

                backgroundPanel.Visibility = Visibility.Collapsed;
                settingsPanel.Visibility = Visibility.Collapsed;
            }

            else if(button == "setting3Button")
            {
                map.MapElements.Clear();

                limit = 5000;
                GetVetsRequest();

                backgroundPanel.Visibility = Visibility.Collapsed;
                settingsPanel.Visibility = Visibility.Collapsed;
            }

            else if (button == "exitButtonForSettings")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                settingsPanel.Visibility = Visibility.Collapsed;
            }
        }

        private async void vetPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var selectedVet = (sender as Grid).DataContext as VetModel;

            if (selectedVet.Color != "gray")
            {
                VetDetailsView.vet = selectedVet;
                Frame.Navigate(typeof(VetDetailsView));
            }

            else
            {
                vetPanel.Visibility = Visibility.Collapsed;
                await Launcher.LaunchUriAsync(new Uri("ms-drive-to:?destination.latitude=" + selectedVet.Location.Latitude.ToString().Replace(",", ".") + "&destination.longitude=" + selectedVet.Location.Longitude.ToString().Replace(",", ".") + "&destination.name=" + selectedVet.Name));
            }
        }

        private void cancelButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            vetPanel.Visibility = Visibility.Collapsed;
        }

        private void callButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI(VetDetailsView.vet.CellNumber.Replace("-", ""), VetDetailsView.vet.Name);
        }

        private async void mapButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            vetPanel.Visibility = Visibility.Collapsed;
            await Launcher.LaunchUriAsync(new Uri("ms-drive-to:?destination.latitude=" + VetDetailsView.vet.Location.Latitude.ToString().Replace(",", ".") + "&destination.longitude=" + VetDetailsView.vet.Location.Longitude.ToString().Replace(",", ".") + "&destination.name=" + VetDetailsView.vet.Name));

        }

        #endregion
    }
}