using Parse;
using System;
using System.Threading.Tasks;
using VetMapp.Core;
using VetMapp.Helpers;
using VetMapp.Models;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace VetMapp.Views
{
    public sealed partial class PetView : Page
    {
        public static PetModel pet;
        public static bool isUpdate;
        private byte[] petImage;


        public PetView()
        {
            this.InitializeComponent();
            this.Loaded += PetView_Loaded;
        }


        private void PetView_Loaded(object sender, RoutedEventArgs e)
        {
            kindCombobox.ItemsSource = PetHelper.AnimalKinds();

            if (isUpdate == true)
            {
                addPetButton.Visibility = Visibility.Collapsed;
                closeButton.Visibility = Visibility.Collapsed;
                birthdateDatePicker.Date = pet.BirthDate.Date;
                DataContext = pet;
                petInformation.DataContext = pet;
            }

            else
            {
                updateButton.Visibility = Visibility.Collapsed;
                deleteButton.Visibility = Visibility.Collapsed;
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }


        private async void addPetButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            progress.IsIndeterminate = true;

            try
            {
                if (!string.IsNullOrEmpty(name.Text) && kindCombobox.SelectedItem != null && breedcombobox.SelectedItem != null)
                {
                    var model = new PetModel()
                    {
                        Name = name.Text,
                        Kind = kindCombobox.SelectedItem.ToString(),
                        Breed = breedcombobox.SelectedItem.ToString(),
                        User = ParseUser.CurrentUser,
                        BirthDate = birthdateDatePicker.Date.DateTime
                    };

                    if (petImage != null)
                    {
                        ParseFile file = new ParseFile("petImage", petImage);
                        await file.SaveAsync();
                        model.Picture = file;
                    }

                    await DataClient.Instance.AddPet(model);

                    backgroundPanel.Visibility = Visibility.Visible;
                    addInfoPanel.DataContext = model.Name;
                    addInfoPanel.Visibility = Visibility.Visible;
                }
            }

            catch (Exception)
            {

            }

            progress.IsIndeterminate = false;
        }

        private void closeButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void updateButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            progress.IsIndeterminate = true;

            try
            {
                if (!string.IsNullOrEmpty(name.Text) && kindCombobox.SelectedItem != null && breedcombobox.SelectedItem != null)
                {
                    var model = new PetModel()
                    {
                        ObjectId = pet.ObjectId,
                        Name = name.Text,
                        Kind = kindCombobox.SelectedItem.ToString(),
                        Breed = breedcombobox.SelectedItem.ToString(),
                        User = ParseUser.CurrentUser,
                        BirthDate = birthdateDatePicker.Date.DateTime
                    };

                    if (petImage != null)
                    {
                        ParseFile file = new ParseFile("petImage", petImage);
                        await file.SaveAsync();
                        model.Picture = file;
                    }
                    
                    await DataClient.Instance.UpdatePet(model);

                    backgroundPanel.Visibility = Visibility.Visible;
                    updateInfoPanel.DataContext = model.Name;
                    updateInfoPanel.Visibility = Visibility.Visible;
                }

            }

            catch (Exception)
            {
            }

            progress.IsIndeterminate = false;
        }

        private void deleteButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            backgroundPanel.Visibility = Visibility.Visible;
            deletePanel.DataContext = pet.Name;
            deletePanel.Visibility = Visibility.Visible;
        }


        private void button_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = (sender as Grid).Name;

            if (button == "acceptButton")
            {
                progress.IsIndeterminate = true;

                try
                {
                    deletePanel.Visibility = Visibility.Collapsed;
                    deleteInfoPanel.DataContext = pet.Name;
                    deleteInfoPanel.Visibility = Visibility.Visible;
                }

                catch (Exception)
                {
                }

                progress.IsIndeterminate = false;
            }

            else if (button == "exitButtonForDeletePanel")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                deletePanel.Visibility = Visibility.Collapsed;
            }

            else if (button == "closeButtonForDeleteInfoPanel")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                deleteInfoPanel.Visibility = Visibility.Collapsed;
                Frame.GoBack();
            }

            else if (button == "closeButtonForAddInfoPanel")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                addInfoPanel.Visibility = Visibility.Collapsed;
                Frame.GoBack();
            }

            else if (button == "closeButtonForUpdateInfoPanel")
            {
                backgroundPanel.Visibility = Visibility.Collapsed;
                updateInfoPanel.Visibility = Visibility.Collapsed;
                Frame.GoBack();
            }
        }

        private async void pictureButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            try
            {
                CameraCaptureUI captureUI = new CameraCaptureUI();
                captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;

                StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

                IRandomAccessStream imageStream;
                using (IRandomAccessStream fileStream = await photo.OpenAsync(FileAccessMode.Read))
                {
                    imageStream = fileStream.CloneStream();
                }
                
                petImage = await ConvertToArray(imageStream);
            }

            catch (Exception)
            {

            }
        }

        private async Task<byte[]> ConvertToArray(IRandomAccessStream stream)
        {
            var dataReader = new DataReader(stream.GetInputStreamAt(0));
            var bytes = new byte[stream.Size];

            await dataReader.LoadAsync((uint)stream.Size);
            dataReader.ReadBytes(bytes);

            return bytes;
        }

        private void kindCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selected = kindCombobox.SelectedItem.ToString();

            if (selected == "Kedi") breedcombobox.ItemsSource = PetHelper.catBreeds();
            else if (selected == "Köpek") breedcombobox.ItemsSource = PetHelper.dogBreeds();
            else if (selected == "Kuş") breedcombobox.ItemsSource = PetHelper.birdBreeds();
            else if (selected == "Diğer") breedcombobox.ItemsSource = PetHelper.otherBreeds();
        }
    }
}